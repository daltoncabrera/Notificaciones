using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using ccpsd.notificaciones.core;

namespace ccpsd.notificaciones.client
{
    public partial class SettingDlg : Form
    {  
        private IContainer m_Container = null;
        private NotifyIcon m_NotifyIcon = null;
        private Button btnHide;
        private ContextMenu m_ContextMenu = null;
        private NotifMonitor notifMonitor;
        private  static bool _logActive = true;

        public delegate void CloseNotificacion(CloseReason closeReason, int notiLogId);

        public event CloseNotificacion OnCloseNotificacion = null;

        public delegate void NotificacionShown( int notiLogId);

        public event NotificacionShown OnNotificacionShown = null;





        public NotifyIcon NotifyIcon
        {
            get { return m_NotifyIcon; }
        }

        /// <summary>
        /// Start the UI thread
        /// </summary>
        public static SettingDlg StartUIThread()
        {
            SettingDlg dlg = new SettingDlg();
            dlg.Visible = false;
            Thread thread = new Thread(new ThreadStart(dlg.UIThread));
            thread.Start();
            return dlg;
        }

        /// <summary>
        /// UI thread
        /// </summary>
        public void UIThread()
        {
       
            Application.Run(this);
        }

        public SettingDlg()
        {
            InitializeComponent();

        }

     
        /// <summary>
        /// Move the window to the right-bottom corner
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Left
                + Screen.PrimaryScreen.WorkingArea.Width
                - this.Width
                ;
            this.Top = Screen.PrimaryScreen.WorkingArea.Top
                + Screen.PrimaryScreen.WorkingArea.Height
                - this.Height
                ;


            this.Visible = false;
        }

        public void OnShowNotification(NotificacionesLogModel notifCliente)
        {
            PopupNotify p = new PopupNotify();
            p.WaitTime = 0;
            p.Title = string.Format("[{0}] {1}",notifCliente.Aplicacion, notifCliente.Titulo);
            p.Message = notifCliente.Nota;
            p.Link = notifCliente.Link;
            p.ShowForever = true;
            p.NotiLogId = notifCliente.IdNotificacionLog;
            p.OnCloseNotification += p_OnCloseNotification;
            p.OnNotificationShown += p_OnNotificationShown;
            p.WaitOnMouseOver = true;
            this.Invoke(new MethodInvoker(()=>
                                              {
                                                  p.Show();
                                              }));
        }

        void p_OnNotificationShown(int notiLogId)
        {
            if (OnNotificacionShown != null)
                OnNotificacionShown(notiLogId);
        }

        void p_OnCloseNotification(CloseReason closeReason, int notiLogId)
        {
            if (OnCloseNotificacion != null)
                OnCloseNotificacion(closeReason, notiLogId);
        }


        private void SettingDlg_Load(object sender, EventArgs e)
        {
            m_ContextMenu = new ContextMenu();
            m_ContextMenu.MenuItems.Add(new MenuItem("Abrir", this.OpenDialog));
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDlg));
            Icon icon = ((System.Drawing.Icon)(resources.GetObject("ctlNotifyIcon.Icon")));
            m_Container = new Container();
            m_NotifyIcon = new NotifyIcon(m_Container);
            m_NotifyIcon.Text = "CCPSD Notificaciones";
            m_NotifyIcon.ContextMenu = m_ContextMenu;
            m_NotifyIcon.Icon = icon;
            m_NotifyIcon.Visible = true;
            m_NotifyIcon.ContextMenu = m_ContextMenu;
            m_NotifyIcon.ShowBalloonTip(200
                , "ccpsd.notificaciones.service"
                , "Servicio para recibir las notificaciones de la camara"
                , ToolTipIcon.Info
                );

            textBox1.Text = ConfigReader.GetServerUrl().Replace("/Signalr", "");

        }


        public void OpenDialog(Object sender, EventArgs e)
        {
            this.Visible = true;
            BringToFront();
        }

        protected override void OnClosed(EventArgs e)
        {
            /// m_NotifyIcon.Dispose();
            //m_ContextMenu.Dispose();
            //m_Container.Dispose();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public void SetTitleText(string text)
        {
            this.Invoke(new MethodInvoker(() =>
                                              {
                                                  this.Text  = string.Format("CCPSD Notificaciones [{0}]", text);
                                              }));
        }

        private void btnSaveAndConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var oldServer = ConfigReader.GetServerUrl();
                if (oldServer != textBox1.Text)
                {
                    ConfigReader.SaveServerUrl(textBox1.Text);
                }

                Utils.RestarCurrentApp();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No tiene permisos para configurar el cliente, favor contacte con un administrador...");
            }
           
        }

        private void checkBoxLogActive_CheckedChanged(object sender, EventArgs e)
        {
            _logActive = checkBoxLogActive.Checked;
        }


        public void ShowLog(string text)
        {
            if (_logActive)
            {
                NotifyIcon.BalloonTipText = text;
                NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                NotifyIcon.ShowBalloonTip(Constantes.IntervalorDefault);
            }

            SetTitleText(text);
        }
    }
}
