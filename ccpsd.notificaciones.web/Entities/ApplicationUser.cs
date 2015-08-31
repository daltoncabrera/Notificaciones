using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ccpsd.notificaciones.core;

namespace ccpsd.notificaciones.web.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        

        [Required]
        public byte Level { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }


        [Required]
        public Boolean Active { get; set; }
        public String ImgName { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }


        public static UserModel GetFromEntity(ApplicationUser user)
        {
            return new UserModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                JoinDate = user.JoinDate,
                Active = user.Active,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                ImgName = user.ImgName
            };
        }

        public static ApplicationUser FillEntitie(UserModel userModel)
        {
            return new ApplicationUser
            {
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                JoinDate = userModel.JoinDate,
                Active = userModel.Active,
                EmailConfirmed = userModel.EmailConfirmed,
                Id = userModel.Id,
                ImgName = userModel.ImgName

            };
        }
    }
}