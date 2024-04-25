using MyBlogApi;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args).RegisterServices();
        var app = builder.Build().SetupMiddleware();
        app.Run();
    }
}



