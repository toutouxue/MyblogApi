using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyBlogApi.Store;
using MyBlogApi;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Text;

namespace MyBlogApi
{
    public static class Startup
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ArticleBase, Article>();
            builder.Services.AddDbContext<MyBlogApiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });
            builder.Services.AddScoped<ILogger>();
            builder.Services.AddScoped<ApplicationUser>();
            builder.Services.AddScoped<ApplicationUserStore>();
            builder.Services.AddScoped<IArticleStore, ArticleStore>();
            builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores< MyBlogApiContext >();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services
                .AddAuthentication("Bearer")
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new() 
                {

                    ValidateIssuer = true,
                    RequireExpirationTime= true,
                    RequireSignedTokens = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                };
            });

            //builder.Services.AddTransient(provider =>
            //{
            //    var dbContext = provider.GetService(typeof(MyBlogApiContext)) as MyBlogApiContext;
            //    var userStore = new ApplicationUserStore(dbContext);

            //    return new ArticleAndUserSeeder(provider, dbContext, new ApplicationUserStore(dbContext));
            //} );
            
            builder.Services.AddTransient<ArticleAndUserSeeder>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    //如果添加了AllowCredentials，需要显式设置Origins
                    builder => builder.WithOrigins("https://*:433")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials());
            });
            return builder;
        }
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                RunSeeding(app.Services);

                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img")),
                    RequestPath = "/Img"
                }
               );
            }
            else { 
                app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img")),
                    RequestPath = "/Img"
                });
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}"
                );
            });
            
            return app;
        }
        public static void RunSeeding(IServiceProvider services)
        {
            //var seeder = services.GetService<ArticleAndUserSeeder>()??throw new ArgumentNullException(nameof(ArticleAndUserSeeder),"ArticleAndUserSeeder 不能为空") ;
            //seeder.Seed();
            //seeder.SeedTestData();
        }
    }
}
