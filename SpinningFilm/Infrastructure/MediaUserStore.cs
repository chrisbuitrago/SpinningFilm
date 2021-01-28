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
    public class MediaUserStore : IUserStore<MediaUser>, IUserPasswordStore<MediaUser>, IUserEmailStore<MediaUser>, IUserSecurityStampStore<MediaUser>
    {

        private readonly SpinningFilmContext _context;
        public MediaUserStore(SpinningFilmContext context)
        {
            _context = context;
        }

        #region IUserStore
        public Task<IdentityResult> CreateAsync(MediaUser user, CancellationToken cancellationToken)
        {
            _context.Add(user);
            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(MediaUser user, CancellationToken cancellationToken)
        {
            var appUser = _context.MediaUsers.FirstOrDefault(u => u.Id == user.Id);

            if (appUser != null)
            {
                _context.MediaUsers.Remove(appUser);
                _context.SaveChanges();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public Task<MediaUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.MediaUsers.FirstOrDefault(u => u.Id == userId));
        }

        public Task<MediaUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.MediaUsers.FirstOrDefault(u => u.NormalizeEmail == normalizedUserName));
        }

        public Task<string> GetNormalizedUserNameAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeEmail);
        }

        public Task<string> GetUserIdAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(MediaUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizeEmail = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(MediaUser user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(MediaUser user, CancellationToken cancellationToken)
        {
            var appUser = _context.MediaUsers.FirstOrDefault(u => u.Id == user.Id);
            
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
        public Task<bool> HasPasswordAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task<string> GetPasswordHashAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task SetPasswordHashAsync(MediaUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
        #endregion

        #region IUserEmailStore
        public Task<MediaUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.MediaUsers.FirstOrDefault(u => u.Email == email));
        }
        public Task<string> GetEmailAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeEmail);
        }

        public Task SetEmailAsync(MediaUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(MediaUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(MediaUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizeEmail = normalizedEmail;
            return Task.CompletedTask;
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(MediaUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(MediaUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }
        #endregion
    }
}
