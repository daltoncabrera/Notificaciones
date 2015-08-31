using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using Newtonsoft.Json;
using ccpsd.notificaciones.core;

namespace ccpsd.notificaciones.client
{
    public class SignalCliente
    {
        private HubConnection hubConnection = null;
       
        private IHubProxy cHubProxy = null;



        public delegate void ConnectionStateChange(ConnectionState conState);

        public event ConnectionStateChange OnConnectionStateChange = null;

        public delegate void RecibirNotificaciones(NotificacionesLogModel  notiModel);

        public event RecibirNotificaciones OnRecibirNotificaciones;

        public delegate void LogMessages(string message);
        public event LogMessages OnLogMessages = null;



        public ConnectionState ConnectionState {
            get { return hubConnection.State; }
        }

        public SignalCliente(string serverUrl)
        {
            hubConnection = new HubConnection(serverUrl, useDefaultUrl:false);
            cHubProxy = hubConnection.CreateHubProxy("NotificacionesHub");
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = Console.Out;
            hubConnection.Error += hubConnection_Error;
            hubConnection.Closed +=hubConnection_Closed;
            cHubProxy.On<NotificacionesLogModel>("OnDispachNotification", RecibirNotifciacionDelServidor);

            cHubProxy.On("heartbeat", OnHeartBeat);

            hubConnection.ConnectionId = Environment.UserName;
            hubConnection.StateChanged += hubConnection_StateChanged;
        
        }

        void hubConnection_Error(Exception obj)
        {
            dispatchState(ConnectionState.Disconnected);
        }



        void hubConnection_Closed()
        {
            OnConnectionStateChange(ConnectionState.Disconnected);
        }

        void hubConnection_StateChanged(StateChange objState)
        {
            dispatchState(objState.NewState);
        }

        public  void Connect()
        {
            
            if (hubConnection.State == ConnectionState.Disconnected
                && hubConnection.State != ConnectionState.Reconnecting)
            {

                  hubConnection.Start().ContinueWith(task =>
                                                       {
                                                           if (task.IsFaulted)
                                                           {
                                                               if (task.Exception.InnerException != null && task.Exception.InnerException.InnerException != null)
                                                               {
                                                                   logMsg(task.Exception.InnerException.InnerException.Message);
                                                               }
                                                               dispatchState(ConnectionState.Disconnected);
                                                           }
                                                     
                                                       }).Wait();
            }
        }

        public void Disconnect()
        {
            if (hubConnection != null && hubConnection.State == ConnectionState.Connected)
            {
                hubConnection.Stop();
            }
        }


        private void OnHeartBeat()
        {
            logMsg("Log del server");
        }

        private void dispatchState(ConnectionState objState)
        {
            if (OnConnectionStateChange != null)
            {
                OnConnectionStateChange(objState);
            }
        }



        public async  Task RegistrarLecturaNotificacion(int IdNotificacionLog, string usuario, string key)
        {

            var result = 0;
            if (hubConnection.State == ConnectionState.Connected)
            {
                cHubProxy.Invoke<int>("RegistrarLecturaNotificacion", IdNotificacionLog, usuario, key)
                    .ContinueWith(task =>
                                      {
                                          if (task.IsFaulted)
                                          {
                                              if (task.Exception != null)
                                                  logMsg(task.Exception.Message);
                                          }
                                      });
            }
        }


        public async Task<List<NotificacionesLogModel>> GetNotificacionesPorMostrar(string username)
        {
            List<NotificacionesLogModel> result = null;
            try
            {
                if (hubConnection.State == ConnectionState.Connected)
                {
                    result = await cHubProxy.Invoke<List<NotificacionesLogModel>>("GetNotificacionesPorMostrar", username);
                    RecibirNotifciacionDelServidor(result);
                }
       
            }
            catch (Exception ex)
            {
                logMsg(ex.Message);
            }
          
            return  result;
        }

        private void RecibirNotifciacionDelServidor(List<NotificacionesLogModel> result)
        {
            foreach (var notificacionesLogModel in result)
            {
                RecibirNotifciacionDelServidor(notificacionesLogModel);
            }
        }

        private void logMsg(string msg)
        {
            if (OnLogMessages != null)
                OnLogMessages(msg);
        }



        private void RecibirNotifciacionDelServidor(NotificacionesLogModel objNotification)
        {
            if (OnRecibirNotificaciones != null)
            {
                OnRecibirNotificaciones(objNotification);
            }
        }

        public void Stop()
        {
            hubConnection.Stop();
        }
    }

   
}
