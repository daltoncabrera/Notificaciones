using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web;
using ccpsd.notificaciones.web.Infrastructure;

namespace WebApiAungularWithPushNoti.Controllers
{
    public abstract class ApiControllerWithHub : ApiController
    {

       protected  NotificacionesRepository _repoNotificaciones;

        protected IHubContext HubContext
        {
            get { return GlobalHost.ConnectionManager.GetHubContext<NotificacionesHub>(); }
        }


        public void DispachNotification(NotificacionesLogModel objNotificacion)
        {
           // HubContext.Clients.Group(objNotificacion.Usuario).OnDispachNotification(objNotificacion);
           HubContext.Clients.All.OnDispachNotification(objNotificacion);
        }

        internal void DispachNotification(int idNotificacion)
        {
            var notiLogsList = _repoNotificaciones.GetNotificacionesLogs(idNotificacion);
            foreach (var notiLogModel in notiLogsList)
            {
                DispachNotification(notiLogModel);
            }
        }


      

    }

}