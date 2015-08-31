// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace ccpsd.notificaciones.web.Entities
{
    // Notificacion
    internal partial class NotificacionConfiguration : EntityTypeConfiguration<Notificacion>
    {
        public NotificacionConfiguration(string schema = "")
        {
            ToTable("Notificacion");
            HasKey(x => x.NotificacionId);

            Property(x => x.NotificacionId).HasColumnName("NotificacionId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AplicacionId).HasColumnName("AplicacionId").IsRequired();
            Property(x => x.Tipo).HasColumnName("Tipo").IsRequired();
            Property(x => x.Link).HasColumnName("Link").IsRequired().HasMaxLength(4000);
            Property(x => x.DuracionEnPantalla).HasColumnName("DuracionEnPantalla").IsRequired();
            Property(x => x.Intervalo).HasColumnName("Intervalo").IsRequired();
            Property(x => x.Vigencia).HasColumnName("Vigencia").IsRequired();
            Property(x => x.TipoVigencia).HasColumnName("TipoVigencia").IsRequired();
            Property(x => x.Estatus).HasColumnName("Estatus").IsRequired();
            Property(x => x.FechaCreacion).HasColumnName("FechaCreacion").IsRequired();
            Property(x => x.FechaCierre).HasColumnName("FechaCierre").IsOptional();
            Property(x => x.Activo).HasColumnName("Activo").IsRequired();
            Property(x => x.Nota).HasColumnName("Nota").IsOptional().HasMaxLength(4000);
            Property(x => x.Usuarios).HasColumnName("Usuarios").IsOptional().HasMaxLength(4000);
            Property(x => x.Titulo).HasColumnName("Titulo").IsOptional().HasMaxLength(255);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
