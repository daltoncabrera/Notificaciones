using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using ccpsd.notificaciones.core;
using System.Runtime.InteropServices;

namespace ccpsd.notificaciones.client
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    public class NotifMonitor
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        private System.Timers.Timer monTimer = null;
        private System.Timers.Timer reconectionTimer = null;
        private SignalCliente cliente = null;
        private string userName;

        public delegate void ShowNotification(NotificacionesLogModel notifCliente);
        public event ShowNotification OnShowNotification = null;

        public delegate void Log(string msg);

        public event Log OnLog = null;

        public SignalCliente SignalCliente
        {
            get { return this.cliente; }
        }

        public NotifMonitor()
        {
            this.userName = Environment.UserName;
            this.cliente = new SignalCliente(ConfigReader.GetServerUrl());
            this.cliente.OnRecibirNotificaciones += ClienteOnOnRecibirNotificaciones;
            this.cliente.OnConnectionStateChange += cliente_OnConnectionStateChange;
            this.cliente.OnLogMessages += cliente_OnLogMessages;

            monTimer = new System.Timers.Timer();
            monTimer.Interval = Constantes.IntervalorDefault;
            monTimer.Elapsed += monTimer_Tick;

            reconectionTimer = new System.Timers.Timer();
            reconectionTimer.Enabled = false;
            reconectionTimer.Interval = 30 * 1000;
            reconectionTimer.Elapsed += reconectionTimer_Elapsed;
        }

        void cliente_OnLogMessages(string message)
        {
            if (OnLog != null)
                OnLog(message);
        }

        private void ClienteOnOnRecibirNotificaciones(NotificacionesLogModel notiModel)
        {
            if (OnShowNotification != null)
                OnShowNotification(notiModel);
        }


        private void ClientConnect(bool force = false)
        {

            if (force)
            {
                this.cliente = new SignalCliente(ConfigReader.GetServerUrl());
                this.cliente.OnRecibirNotificaciones += showNotificaciones;
                this.cliente.OnConnectionStateChange += cliente_OnConnectionStateChange;

            }


            if (cliente.ConnectionState == ConnectionState.Disconnected)
            {
                cliente.Connect();
            }
        }

        void reconectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            reconectionTimer.Stop();
            ClientConnect();

        }

        void cliente_OnConnectionStateChange(ConnectionState conState)
        {
            switch (conState)
            {
                case ConnectionState.Connected:
                    Loggin("Conectado");
                    CheckNotificaciones();
                    break;

                case ConnectionState.Disconnected:
                    reconectionTimer.Start();
                    Loggin("Desconectado");
                    break;


                case ConnectionState.Connecting:
                    Loggin("Conectando...");
                    break;

                case ConnectionState.Reconnecting:
                    Loggin("Reconectando...");
                    break;


                default:
                    //Loggin("other");
                    break;

            }
        }

        private void Loggin(string log)
        {
            if (OnLog != null)
            {
                OnLog(log);
            }
        }


        public void RunTick()
        {
            monTimer_Tick(null, null);
        }

        void monTimer_Tick(object sender, EventArgs e)
        {
          //  monTimer.Stop();
          //  monTimer.AutoReset = true;
          //  CheckNotificaciones();
          //  monTimer.Start();
        }

        private void CheckNotificaciones()
        {
            try
            {
                var usuarioPamao = GetInactiveTime();
                if (usuarioPamao == null || usuarioPamao.Value.Minutes < Constantes.TiempoUsuarioInactivo)
                {
                    var notifToShowList = cliente.GetNotificacionesPorMostrar(Environment.UserName);

                    foreach (var notiModel in notifToShowList.Result)
                    {
                        showNotificaciones(notiModel);
                    }
                }
            }
            catch (Exception ex)
            {
                //try catch vacio, jodanse.
                MessageBox.Show(ex.Message);
            }
        }

        private void showNotificaciones(NotificacionesLogModel notiModel)
        {

            if (OnShowNotification != null)
            {
                OnShowNotification(notiModel);
                //cliente.RegistrarLecturaNotificacion(notiModel.IdNotificacionLog, notiModel.Usuario, "");
            }

        }

        public void Run(bool force = false)
        {
            ClientConnect(force);
            monTimer.Start();
        }

        public void Stop()
        {
            monTimer.Stop();
            cliente.Stop();
        }

        public TimeSpan? GetInactiveTime()
        {
            LASTINPUTINFO info = new LASTINPUTINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            if (GetLastInputInfo(ref info))
                return TimeSpan.FromMilliseconds(Environment.TickCount - info.dwTime);
            else
                return null;
        }

        public void Reset()
        {
            ClientConnect(true);
        }
    }
}
