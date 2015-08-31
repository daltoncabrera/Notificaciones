using ccpsd.notificaciones.core;

namespace ccpsd.notificaciones.web.Migrations
{
    using ccpsd.notificaciones.web.Entities;
    using ccpsd.notificaciones.web.Infrastructure;
    using ccpsd.notificaciones.web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<NotificacionesContext>
    {
        public Configuration()
        {

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            //Database.SetInitializer<INotificacionesContext>(new CreateDatabaseIfNotExists<INotificacionesContext>());

            Database.SetInitializer<INotificacionesContext>(new DropCreateDatabaseIfModelChanges<INotificacionesContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<INotificacionesContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        protected override async void Seed(NotificacionesContext context)
        {
            if (context.Clients.Count() <= 0)
            {
                BuildClientsList().ForEach(s =>
                {
                    context.Clients.Add(s);

                });
                context.SaveChanges();

            }

         var createdAdmin =  await CreateAdmin();

        }

        public static async Task<IdentityResult> CreateAdmin()
        {
            try {
                var _repo = new AuthRepository();
                var adminExistente = _repo.FindAsync("admin");
                await adminExistente;
                if (adminExistente.Result == null)
                {
                    var user = new UserModel()
                    {
                        UserName = "admin",

                        // Add the following so our Seed data is complete:
                        FirstName = "System",
                        LastName = "Admin",
                        Email = "admin@Example.com",
                        Password = "Aa123!",
                        ConfirmPassword = "Aa123!",
                        Active = true,
                        EmailConfirmed = true

                    };

                    return await _repo.RegisterUser(user);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        private static List<Client> BuildClientsList()
        {

            List<Client> ClientsList = new List<Client>
            {
                new Client
                {
                    Name = "webnotificaciones",
                    Secret = "5YV7M1r981yoGhELyB84aC+KiYksxZf1OY3++C1CtRM=",
                    Description="Notificaciones Web",
                    ApplicationType =  ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                    
                },
                new Client
                {
                    Name = "AppApiConsumer",
                    Secret = "Dzm5Pk2Sq96dn8GpbORoP7stDHXPTrhJaaJc3k5ocRg=",
                    Description="App para conusmir el api",
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Name = "testapp",
                    Secret = "lCXDroz4HhR1EIx8qaz3C13z/quTXBkQ3Q5hj7Qx3aA=",
                    Description="Application de Prueba",
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}
