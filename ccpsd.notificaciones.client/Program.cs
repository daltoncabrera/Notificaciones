using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using WindowsFormsApplication1;
using ccpsd.notificaciones.core;

namespace ccpsd.notificaciones.client
{
    static class Program
    {

        private static SignalCliente _signalCliente = null;
        private static Svc _Scv = null;
        private static string _CurrentUser = null;
        private static bool _FirstConection = true;
        private static System.Timers.Timer reconectionTimer = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //Application.Run(new Form1());
            //return;
            _Scv = new Svc();
            _CurrentUser = Environment.UserName;
            _Scv.Start();
            InitReactor();
        }


        static void InitReactor()
        {
            try
            {
                _signalCliente = new SignalCliente(ConfigReader.GetServerUrl());
                _signalCliente.OnLogMessages += Client_OnLog;
                _signalCliente.OnConnectionStateChange += _signalCliente_OnConnectionStateChange;
                _signalCliente.OnRecibirNotificaciones += _signalCliente_OnRecibirNotificaciones;
                _Scv.SettingDlg.OnCloseNotificacion += SettingDlg_OnCloseNotificacion;
                _Scv.SettingDlg.OnNotificacionShown += SettingDlg_OnNotificacionShown;
                _signalCliente.Connect();

                reconectionTimer = new System.Timers.Timer();
                reconectionTimer.Enabled = false;
                reconectionTimer.Interval = Constantes.IntervalorDefault;
                reconectionTimer.Elapsed += reconectionTimer_Elapsed;
                reconectionTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        static void reconectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            if(_signalCliente.ConnectionState ==ConnectionState.Disconnected)
            {
                reconectionTimer.Stop();
                try
                {
                    _signalCliente.Connect();
                }
                catch (Exception ex)
                {
                  Client_OnLog(ex.Message);
                }   
                reconectionTimer.Start();
            }
        }

        static void SettingDlg_OnNotificacionShown(int notiLogId)
        {
           
             //_signalCliente.RegistrarLecturaNotificacion(notiLogId, _CurrentUser, string.Empty);
        }

        static void SettingDlg_OnCloseNotificacion(CloseReason closeReason, int notiLogId)
        {
            if(closeReason == CloseReason.UserClosing)
                _signalCliente.RegistrarLecturaNotificacion(notiLogId, _CurrentUser, string.Empty);
        }

        static void _signalCliente_OnRecibirNotificaciones(NotificacionesLogModel notiModel)
        {
            if(notiModel.Usuario.Equals(_CurrentUser))
                _Scv.SettingDlg.OnShowNotification(notiModel);
        }

        

        
        static void Client_OnLog(string msg)
        {
            _Scv.SettingDlg.ShowLog(msg);
        }

        static void _signalCliente_OnConnectionStateChange(ConnectionState conState)
        {

            switch (conState)
            {
                case ConnectionState.Connected:
                    Client_OnLog("Conectado");
                    _signalCliente.GetNotificacionesPorMostrar(_CurrentUser);
                    break;

                case ConnectionState.Disconnected:
                    // reconectionTimer.Start();
                    Client_OnLog("Desconectado");
                   // Thread.Sleep(Constantes.IntervalorDefault);
                    //_signalCliente.Connect();
                    break;


                case ConnectionState.Connecting:
                    Client_OnLog("Conectando...");
                    break;

                case ConnectionState.Reconnecting:
                    Client_OnLog("Reconectando...");
                    break;


                default:
                    //Loggin("other");
                    break;

            }
        }
    }

}
