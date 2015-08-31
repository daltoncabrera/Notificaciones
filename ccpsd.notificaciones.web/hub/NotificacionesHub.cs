using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Infrastructure;

namespace ccpsd.notificaciones.web
{
  
    public class NotificacionesHub : Hub
    {

        private static readonly ConcurrentDictionary<string, UserConnection> Users
       = new ConcurrentDictionary<string, UserConnection>();

        NotificacionesRepository _repoNotificaciones;
        private ClienteRepository _clienteRepository;
      
       // Lazy<IHubContext> hub = new Lazy<IHubContext>(
       //    () => GlobalHost.ConnectionManager.GetHubContext<NotificacionesHub>()
       //);

        //var context = ;

        protected IHubContext HubContext
        {
            get { return GlobalHost.ConnectionManager.GetHubContext<NotificacionesHub>(); }
        }

        public NotificacionesHub()
        {
            _repoNotificaciones = new NotificacionesRepository();
            _clienteRepository = new ClienteRepository();

        }

        public void Heartbeat()
        {
            //  Console.WriteLine("Hub Heartbeat\n");
            Clients.All.heartbeat();
        }


        private bool ValidarAppKey(string key)
        {
            return _clienteRepository.ValidateClientByKey(key);
        }


        public void RegistrarLecturaNotificacion(int idNotificacionLog, string usuario, string key)
        {
            try
            {
                _repoNotificaciones.RegistrarLecturaNotificacion(idNotificacionLog, true);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        throw ex.InnerException.InnerException;

                    }
                    else
                    {
                        throw ex.InnerException;
                    }
                }

                throw ex;
            }

        }


        public void Prueba(string msg)
        {
            Clients.All.prueba(msg);
        }

        public string PruebaReturn()
        {
            return "Ahora es que viene el mamboooooooo";
        }



        public List<NotificacionesLogModel> GetNotificacionesPorMostrar(string userName)
        {
            try
            {   
                var notiList =  _repoNotificaciones.GetNotificacionesLogs(userName);
                return notiList;

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        throw ex.InnerException.InnerException;

                    }
                    else
                    {
                        throw ex.InnerException;
                    }
                }

                throw ex;
            }

        }






        public override Task OnConnected()
        {
            Console.WriteLine("Hub OnConnected {0}\n", Context.ConnectionId);
            addUserConection();
            return (base.OnConnected());
        }

        private void addUserConection()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserConnection()
            {
                Name = userName
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }
        }

        private void RemoveUserConection()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserConnection()
            {
                Name = userName
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Remove(connectionId);
            }
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("Hub OnReconnected {0}\n", Context.ConnectionId);
            return (base.OnDisconnected(false));
        }

        public Task OnDisconnected()
        {
            Console.WriteLine("Hub OnDisconnected {0}\n", Context.ConnectionId);
            RemoveUserConection();
            return base.OnDisconnected(false);
        }


    }
}