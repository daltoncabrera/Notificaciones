using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccpsd.notificaciones.core
{
    public static class Constantes
    {
        public const string ConfigExecName = @"ccpsd.notificaciones.serveruracion.exe";
        public const string NotificacionExecName = @"ccpsd.notificaciones.exe";
        public const string ConfiguradoValue = "1";
        public const string TipoInstanciaCliente = "0";
        public const string TipoInstanciaServidor = "1";
        public const string TAG_SERVER = "SERVIDOR";
        public const string TAG_SERVER_PORT = "PUERTO";
        public const string TAG_TIPO_INSTANCIA = "TIPO_INSTANCIA";
        public const string TAG_CONFIGURADO = "CONFIGURADO";
        public const string TAG_MACHINE_NAME = "MACHINE_NAME";
        public const string TAG_LDAP_SERVER = "LDAP";
        public const int MAX_NOTIFICACIONES = 5;

        public const int IntervalorDefault = 50000;
        public const int SignalTimeOut = 300000;
        public const int TiempoUsuarioInactivo = 5;
        public static List<string> GetConfigTags()
        {
            var configTags = new List<string>();
            configTags.Add(Constantes.TAG_SERVER);
            configTags.Add(Constantes.TAG_SERVER_PORT);
            configTags.Add(Constantes.TAG_TIPO_INSTANCIA);
            configTags.Add(Constantes.TAG_CONFIGURADO);
            configTags.Add(Constantes.TAG_MACHINE_NAME);

            return configTags;
        }

    }
}
