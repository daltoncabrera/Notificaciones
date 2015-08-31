using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.web.Infrastructure
{
    public class Enumeraciones
    {
        /// <summary>
        /// Los tipos de notificaciones
        /// </summary>
        public enum TiposNotificaciones
        {
            /// <summary>
            /// La notificacion solo es hasta que el usuario la cierre
            /// </summary>
            [Description("Usuario cierra")]
            AlCierreDeUsuario = 1,

            /// <summary>
            /// La notficion se queda en la pantalla hasta que el server la quite
            /// </summary>
            [Description("Fija en pantalla")]
            FijaEnPantalla = 2,

            /// <summary>
            /// se muestra espordicamente segun viejgencia
            /// </summary>
            [Description("Esporadicamente")]
            Esporadica
        }

        public enum TipoVigencia
        {
            [Description("N/A")]
            None = 0,
            [Description("Segundos")]
            Segundos = 1,
            [Description("Minutos")]
            Minutos = 2,
            [Description("Horas")]
            Horas = 3,
            [Description("Dias")]
            Dias = 4,
            [Description("Intervalos")]
            Intervalos = 5
        }


        public enum ResultType
        {
            [Description("Error")]
            Error = 0,
            [Description("Warning")]
            Warning = 1,
            [Description("Info")]
            Info = 2,
            [Description("Success")]
            Success = 3
        }

    }
}