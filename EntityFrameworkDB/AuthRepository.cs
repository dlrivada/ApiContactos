using System;
using System.Threading.Tasks;
using Domain.Model.ContactAggregate;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.EntityFramework
{
    public class AuthRepository : IDisposable
    {
        private bool _disposed;
        private readonly AuthContext _ctx;

        private readonly UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(Usuario userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.Login
            };

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _ctx.Dispose();
                _userManager.Dispose();
            }

            _disposed = true;
        }

    }

}