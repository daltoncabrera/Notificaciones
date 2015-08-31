// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace ccpsd.notificaciones.web.Entities
{
    // NotificacionCliente
    public partial class NotificacionCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificacionClienteId { get; set; } // NotificacionClienteId (Primary key)
        public int NotificacionId { get; set; } // NotificacionId
        public int ClienteId { get; set; } // ClienteId
        public int AplicacionId { get; set; } // AplicacionId
        public int Tipo { get; set; } // Tipo
        public string Link { get; set; } // Link
        public int DuracionEnPantalla { get; set; } // DuracionEnPantalla
        public int Intervalo { get; set; } // Intervalo
        public int Vigencia { get; set; } // Vigencia
        public int TipoVigencia { get; set; } // TipoVigencia
        public DateTime FechaCreacion { get; set; } // FechaCreacion
        public DateTime? FechaCierre { get; set; } // FechaCierre
        public string Usuario { get; set; } // Usuario
        public DateTime? MostradoUltimaVez { get; set; } // MostradoUltimaVez
        public int Contador { get; set; } // Contador
        public bool Mostrado { get; set; } // Mostrado
        public bool Activo { get; set; } // Activo
        public string Nota { get; set; } // Nota
        public DateTime? MostradoPrimeraVez { get; set; } // MostradoPrimeraVez
        public string Titulo { get; set; } // Titulo
    }

}
