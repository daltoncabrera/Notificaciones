using ccpsd.notificaciones.web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.web.Infrastructure
{
    public class INotificacionesContext :  IdentityDbContext<IdentityUser>      
    {
        public INotificacionesContext()
            : base("NotificacionesContext")
        {
            ////Database.SetInitializer<INotificacionesContext>(new CreateDatabaseIfNotExists<INotificacionesContext>());

            //Database.SetInitializer<INotificacionesContext>(new DropCreateDatabaseIfModelChanges<INotificacionesContext>());
            ////Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<INotificacionesContext>());
            ////Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        public IDbSet<Configuracion> Configuracions { get; set; } // Configuracion        
        public IDbSet<Notificacion> Notificacions { get; set; } // Notificacion
        public IDbSet<NotificacionCliente> NotificacionClientes { get; set; } // NotificacionCliente
        public IDbSet<NotificacionesLog> NotificacionesLogs { get; set; } // NotificacionesLog
        public IDbSet<Client> Clients { get; set; }
        public IDbSet<ApplicationUser> ApplicationUsers { get; set; }
        public IDbSet<RefreshToken> RefreshTokens { get; set; }

    }

}