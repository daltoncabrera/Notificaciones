// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace ccpsd.notificaciones.core
{
    // NotificacionesLog
    [Serializable]
    public partial class NotificacionesLogModel
    {
        public NotificacionesLogModel()
        {
            IdTipo = 0;
            IdTipoVigencia = 0;
            Activo = true;
            Mostrado = false;
            Contador = 0;
            FechaCreacion = DateTime.Now;
        }

        public int IdNotificacionLog { get; set; } // IdNotificacionLog (Primary key)
        public int IdNotificacion { get; set; } // IdNotificacion
        public int AplicacionId { get; set; } // AplicacionId        
        public string Aplicacion { get; set; } // Link       
        public string Tipo { get; set; } // TipoVigencia         
        public int IdTipo { get; set; }
        public string Link { get; set; } // Link        
        public int Intervalo { get; set; } // Intervalo
        public string TipoVigencia { get; set; } // Vigencia        
        public int IdTipoVigencia { get; set; } // Vigencia        
        public int Vigencia { get; set; } // Vigencia                
        public string Nota { get; set; } // Nota
        public string Titulo { get; set; } // Titulo
        public string Usuario { get; set; } // Usuario
        public DateTime? MostradoPrimeraVez { get; set; } // MostradoPrimeraVez
        public DateTime? MostradoUltimaVez { get; set; } // MostradoUltimaVez
        public int Contador { get; set; } // Contador
        public bool Activo { get; set; } // Activo
        public DateTime? FechaCierre { get; set; } // FechaCierre
        public bool Mostrado { get; set; } // Mostrado
        public DateTime FechaCreacion { get; set; }

        
        
    }

}
