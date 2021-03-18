using Microsoft.AspNetCore.Identity;
using SpinningFilm.Data;
using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpinningFilm.Infrastructure
{
    public class MediaUserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>, IUserEmailStore<AppUser>, IUserSecurityStampStore<AppUser>
    {

        private readonly SpinningFilmContext _context;
        public MediaUserStore(SpinningFilmContext context)
        {
            _context = context;
        }

        #region IUserStore
        public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            _context.Add(user);
            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            var appUser = _context.AppUsers.FirstOrDefault(u => u.AppUserId == user.AppUserId);

            if (appUser != null)
            {
                _context.AppUsers.Remove(appUser);
                _context.SaveChanges();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.AppUsers.FirstOrDefault(u => u.AppUserId == Guid.Parse(userId)));
        }

        public Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.AppUsers.FirstOrDefault(u => u.NormalizeEmail == normalizedUserName));
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeEmail);
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AppUserId.ToString());
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizeEmail = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            var appUser = _context.AppUsers.FirstOrDefault(u => u.AppUserId == user.AppUserId);
            
            if (appUser != null)
            {
                appUser.NormalizeEmail = user.NormalizeEmail;
                appUser.Email = user.Email;
                appUser.PasswordHash = user.PasswordHash;

                _context.Update(user);
                _context.SaveChanges();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        #endregion

        #region IUserPasswordStore
        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
        #endregion

        #region IUserEmailStore
        public Task<AppUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.AppUsers.FirstOrDefault(u => u.Email == email));
        }
        public Task<string> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeEmail);
        }

        public Task SetEmailAsync(AppUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizeEmail = normalizedEmail;
            return Task.CompletedTask;
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(AppUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }
        #endregion
    }
}
