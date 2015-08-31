using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ccpsd.notificaciones.core;

namespace ccpsd.notificaciones.ApiWinTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                validaCampos();
                var notiApi = new NotificacionesApi(txtServer.Text, txtUser.Text, txtPass.Text);
                var objNotiModel = new NotificacionModel
                                       {
                                           Titulo = txtTitulo.Text,
                                           Nota = txtMsg.Text,
                                           Link = txtLink.Text,
                                           Usuarios = new List<string> {txtUsuarios.Text}
                                       };

                var idNotificacion = notiApi.CrearNotificacion(objNotiModel);
                MessageBox.Show(string.Format("Se creo la notificacion NO. {0} ", idNotificacion));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        private void validaCampos()
        {
            if (string.IsNullOrEmpty(txtServer.Text)
                || string.IsNullOrEmpty(txtTitulo.Text)
                || string.IsNullOrEmpty(txtUser.Text)
                || string.IsNullOrEmpty(txtPass.Text)
                || string.IsNullOrEmpty(txtUsuarios.Text)
                || string.IsNullOrEmpty(txtMsg.Text))
            {
                throw new Exception("Favor completar todos los campos obligatorios para la prueba");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtServer.Text = ConfigReader.GetAppSettingAsString("notificacionesServer");
            txtUser.Text = ConfigReader.GetAppSettingAsString("user");
            txtPass.Text = ConfigReader.GetAppSettingAsString("pass");
        }
    }
}
