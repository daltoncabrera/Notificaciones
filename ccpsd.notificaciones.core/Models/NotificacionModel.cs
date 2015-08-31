using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.core
{
    public class NotificacionModel
    {
        public NotificacionModel()
        {
            IdTipo = 1;
            IdTipoVigencia = 0;
            Activo = true;
        }

        public int? NotificacionId { get; set; } // NotificacionId (Primary key)
        public int AplicacionId { get; set; } // AplicacionId   
        public string Aplicacion { get; set; } // Link        
        public int IdTipo { get; set; }
        public string Tipo { get; set; } // TipoVigencia 
        public string Link { get; set; } // Link
        public int Intervalo { get; set; } // Intervalo
        public int Vigencia { get; set; } // Vigencia
        public string TipoVigencia { get; set; } // Vigencia
        public int IdTipoVigencia { get; set; } // TipoVigencia        
        public DateTime FechaCreacion { get; set; } // FechaCreacion        
        public bool Activo { get; set; } // Activo
        public string Nota { get; set; } // Nota
        public List<string> Usuarios { get; set; } // Usuarios
        public string Titulo { get; set; } // Titulo

     
    }
}