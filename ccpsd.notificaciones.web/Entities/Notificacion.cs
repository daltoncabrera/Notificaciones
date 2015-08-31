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
using System.Linq;
using System.Data.Entity.ModelConfiguration;
using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Infrastructure;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace ccpsd.notificaciones.web.Entities
{
    // Notificacion
    public partial class Notificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificacionId { get; set; }

        // NotificacionId (Primary key)
        public int AplicacionId { get; set; } // AplicacionId        
        public string Aplicacion { get; set; } // Link       
        public string Tipo { get; set; } // TipoVigencia         
        public int IdTipo { get; set; }
        public string Link { get; set; } // Link        
        public int Intervalo { get; set; } // Intervalo
        public string TipoVigencia { get; set; } // Vigencia        
        public int IdTipoVigencia { get; set; } // Vigencia        
        public int Vigencia { get; set; } // Vigencia                
        public DateTime FechaCreacion { get; set; } // FechaCreacion        
        public bool Activo { get; set; } // Activo
        public string Nota { get; set; } // Nota
        public string Usuarios { get; set; } // Usuarios
        public string Titulo { get; set; } // Titulo



        internal static List<NotificacionModel> GetFromEntitie(List<Notificacion> notificationList)
        {
            return notificationList.Select(s => GetFromEntity(s)).ToList();
        }

        public static NotificacionModel GetFromEntity(Notificacion noti)
        {
            return new NotificacionModel
                       {
                           NotificacionId = noti.NotificacionId,
                           AplicacionId = noti.AplicacionId,
                           Aplicacion = noti.Aplicacion,
                           Titulo = noti.Titulo,
                           Link = noti.Link,
                           IdTipo = noti.IdTipo,
                           Tipo = noti.Tipo,
                           IdTipoVigencia = noti.IdTipoVigencia,
                           TipoVigencia = noti.TipoVigencia,
                           Nota = noti.Nota,
                           Vigencia = noti.Vigencia,
                           Usuarios =
                               !string.IsNullOrEmpty(noti.Usuarios) ? noti.Usuarios.StringToList() : new List<string>(),
                           Activo = noti.Activo,
                           FechaCreacion = noti.FechaCreacion,
                           Intervalo = noti.Intervalo
                       };
        }

        public static Notificacion FillEntitie(NotificacionModel notiModel)
        {
            return new Notificacion
                       {
                           NotificacionId = notiModel.NotificacionId.HasValue ? notiModel.NotificacionId.Value : 0,
                           AplicacionId = notiModel.AplicacionId,
                           Aplicacion = notiModel.Aplicacion,
                           Titulo = notiModel.Titulo,
                           Link = notiModel.Link,
                           IdTipo = notiModel.IdTipo,
                           Tipo = notiModel.Tipo,
                           IdTipoVigencia = notiModel.IdTipoVigencia,
                           TipoVigencia = notiModel.TipoVigencia,
                           Nota = notiModel.Nota,
                           Vigencia = notiModel.Vigencia,
                           Usuarios = notiModel.Usuarios != null ? notiModel.Usuarios.ListToString() : null,
                           Activo = notiModel.Activo,
                           FechaCreacion = notiModel.FechaCreacion,
                           Intervalo = notiModel.Intervalo
                       };
        }

        internal static Notificacion UpdateFromModel(Notificacion oldNotificacion, NotificacionModel notificacionModel)
        {
            oldNotificacion.NotificacionId = notificacionModel.NotificacionId.HasValue
                                                 ? notificacionModel.NotificacionId.Value
                                                 : 0;
            oldNotificacion.AplicacionId = notificacionModel.AplicacionId;
            oldNotificacion.Aplicacion = notificacionModel.Aplicacion;
            oldNotificacion.Titulo = notificacionModel.Titulo;
            oldNotificacion.Link = notificacionModel.Link;
            oldNotificacion.Tipo = notificacionModel.Tipo;
            oldNotificacion.IdTipo = notificacionModel.IdTipo;
            oldNotificacion.IdTipoVigencia = notificacionModel.IdTipoVigencia;
            oldNotificacion.TipoVigencia = notificacionModel.TipoVigencia;
            oldNotificacion.Nota = notificacionModel.Nota;
            oldNotificacion.Vigencia = notificacionModel.Vigencia;
            oldNotificacion.Usuarios = notificacionModel.Usuarios != null
                                           ? notificacionModel.Usuarios.ListToString()
                                           : null;
            oldNotificacion.Activo = notificacionModel.Activo;
            oldNotificacion.FechaCreacion = notificacionModel.FechaCreacion;
            oldNotificacion.Intervalo = notificacionModel.Intervalo;

            return oldNotificacion;
        }

    }
}
