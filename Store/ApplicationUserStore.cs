using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlogApi.DTO;
using MyBlogApi;

namespace MyBlogApi.Store
{
    public class ApplicationUserStore
    {
        private readonly MyBlogApiContext dbContext;

        public ApplicationUserStore(MyBlogApiContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var result = await dbContext.ApplicationUser.AddAsync(user, cancellationToken);
            if (await dbContext.SaveChangesAsync() > 0)
                return IdentityResult.Success;
            else return IdentityResult.Failed();

        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var result = dbContext.ApplicationUser.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            if (result.State == EntityState.Deleted)
                return IdentityResult.Success;
            else return IdentityResult.Failed();
        }

        public void Dispose()
        {

        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await dbContext.ApplicationUser.FindAsync(userId);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await dbContext.ApplicationUser.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await dbContext.ApplicationUser.FindAsync(normalizedUserName);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var u = await dbContext.ApplicationUser.FindAsync(user);
            if (u != null)
                return u.UserName;
            return null;
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
