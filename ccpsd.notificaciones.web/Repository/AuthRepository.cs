using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Entities;
using ccpsd.notificaciones.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;


namespace ccpsd.notificaciones.web.Infrastructure
{

    public class AuthRepository : IDisposable
    {
        private NotificacionesContext _ctx;

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManaller;


        public AuthRepository()
        {
            _ctx = new NotificacionesContext();
            var store = new UserStore<ApplicationUser>(_ctx);
            _userManager = new UserManager<ApplicationUser>(store);
            _roleManaller = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {

            if (string.IsNullOrEmpty(userModel.Id))
                userModel.Id = Guid.NewGuid().ToString();

            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                EmailConfirmed = userModel.EmailConfirmed,
                Active = userModel.Active,
                JoinDate = DateTime.Now,
                Id = userModel.Id
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            await _ctx.SaveChangesAsync();
            return result;
        }

        public  List<UserModel> GetUsers()
        {
            var userList = _userManager.Users.ToList();
            return  userList.Select(s => ApplicationUser.GetFromEntity(s)).ToList();
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

      

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.FirstOrDefault(s => s.Name == clientId);

            return client;
        }

    

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }
          
            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           //var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

           //if (refreshToken != null) {
           //    _ctx.RefreshTokens.Remove(refreshToken);
           //    return await _ctx.SaveChangesAsync() > 0;
           //}

           return false;
        }



        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
             return await _ctx.SaveChangesAsync() > 0;
        }

        internal string HashPassword(string password)
        {
            return _userManager.PasswordHasher.HashPassword(password);
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            //var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            //return refreshToken;

            return null;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _ctx.RefreshTokens.ToList();
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<ApplicationUser> FindAsync(string username)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            return user;
        }

 

        internal async Task<IdentityResult> UpdateAsync(UserModel userModel)
        {
            var user = FindUserById(userModel.Id);
            user.UserName = userModel.UserName;
            user.FirstName = user.FirstName;
            user.LastName = user.LastName;
            user.Email = userModel.Email;
            user.EmailConfirmed = userModel.EmailConfirmed;
            user.Active = userModel.Active;
            user.ImgName = userModel.ImgName;
            var result = await _userManager.UpdateAsync(user);
            return  result;
        }

        internal async Task<IdentityResult> DeleteUsuario(string id)
        {
            var user = FindUserById(id);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public ApplicationUser FindUserById(string id)
        {
            return _ctx.ApplicationUsers.FirstOrDefault(s => s.Id == id);
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}