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
using System.Linq;
using ccpsd.notificaciones.core;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace ccpsd.notificaciones.web.Entities
{
    // NotificacionesLog
    public partial class NotificacionesLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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


        internal static List<NotificacionesLog> GetFromEntityFromViewModel(List<NotificacionesLogModel> notiLogsList)
        {
            return notiLogsList.Select(GetFromEntityFromViewModel).ToList();
        }




        internal static List<NotificacionesLogModel> GetModelFromEntity(List<NotificacionesLog> notiLogsList)
        {
            return notiLogsList.Select(GetModelFromEntity).ToList();
        }

        private static NotificacionesLogModel GetModelFromEntity(NotificacionesLog notiLog)
        {
            return  new NotificacionesLogModel
                        {
                            IdNotificacionLog = notiLog.IdNotificacionLog,
                            IdNotificacion =  notiLog.IdNotificacion,
                            Usuario =  notiLog.Usuario,
                            Titulo = notiLog.Titulo,
                            Nota = notiLog.Nota,
                            Intervalo = notiLog.Intervalo,
                            Link = notiLog.Link,
                            Vigencia = notiLog.Vigencia,
                            Contador = notiLog.Contador,
                            Activo =  notiLog.Activo,
                            AplicacionId = notiLog.AplicacionId,
                            Aplicacion = notiLog.Aplicacion,
                            IdTipo = notiLog.IdTipo,
                            Tipo =  notiLog.Tipo,
                            IdTipoVigencia =  notiLog.IdTipoVigencia,
                            TipoVigencia =  notiLog.TipoVigencia,
                            FechaCierre = notiLog.FechaCierre,
                            FechaCreacion = notiLog.FechaCreacion,
                            Mostrado =  notiLog.Mostrado,
                            MostradoPrimeraVez = notiLog.MostradoPrimeraVez,
                            MostradoUltimaVez = notiLog.MostradoUltimaVez
                        };
        }


        private static NotificacionesLog GetFromEntityFromViewModel(NotificacionesLogModel notiLog)
        {
            return new NotificacionesLog
            {
                IdNotificacionLog = notiLog.IdNotificacionLog,
                IdNotificacion = notiLog.IdNotificacion,
                Usuario = notiLog.Usuario,
                Titulo = notiLog.Titulo,
                Nota = notiLog.Nota,
                Intervalo = notiLog.Intervalo,
                Link = notiLog.Link,
                Vigencia = notiLog.Vigencia,
                Contador = notiLog.Contador,
                Activo = notiLog.Activo,
                AplicacionId = notiLog.AplicacionId,
                Aplicacion = notiLog.Aplicacion,
                IdTipo = notiLog.IdTipo,
                Tipo = notiLog.Tipo,
                IdTipoVigencia = notiLog.IdTipoVigencia,
                TipoVigencia = notiLog.TipoVigencia,
                FechaCierre = notiLog.FechaCierre,
                FechaCreacion = notiLog.FechaCreacion,
                Mostrado = notiLog.Mostrado,
                MostradoPrimeraVez = notiLog.MostradoPrimeraVez,
                MostradoUltimaVez = notiLog.MostradoUltimaVez
            };
        }
    }

}
