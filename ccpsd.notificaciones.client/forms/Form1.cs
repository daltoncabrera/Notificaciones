using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using ccpsd.notificaciones.core;
using ccpsd.notificaciones.client;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private HubConnection hubConnection = null;
        private IHubProxy cHubProxy = null;
        private string user = "admin";
        private string pass = "pudrete";
        private SignalCliente siCLiente;
        public Form1()
        {
            InitializeComponent();
            hubConnection = new HubConnection("http://localhost:26264/Signalr", useDefaultUrl: false);
            cHubProxy = hubConnection.CreateHubProxy("NotificacionesHub");
            cHubProxy.On<string>("prueba", sendMessage);
            hubConnection.Start().ContinueWith(task =>
                                                   {
                                                       if (!task.IsFaulted)
                                                       {
                                                           cHubProxy.Invoke("Prueba", "hola");
                                                       }
                                                       else
                                                       {
                                                           MessageBox.Show("Error :" + task.Exception.Message);
                                                       }
                                                   });

            siCLiente = new SignalCliente("http://localhost:26264/Signalr");
        }

        private void sendMessage(string message)
        {

            this.Invoke(new Action(() =>
                                       {
                                           listBox1.Items.Add(string.Format("Esto es lo que enviaste :{0}",  message));    
                                       }));

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cHubProxy.Invoke("Prueba",textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cHubProxy.Invoke<List<NotificacionesLogModel>>("GetNotificacionesPorMostrar", "dcabrera")
            // .ContinueWith(task =>
            // {
            //     sendMessage(JsonConvert.SerializeObject(task.Result));
            // });

           siCLiente.Connect();
            var lista = siCLiente.GetNotificacionesPorMostrar("dcabrera");

        }
    }
}
