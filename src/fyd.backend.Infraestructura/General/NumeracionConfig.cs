using fyd.backend.Infraestructura.General.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Infraestructura.General
{
    public class NumeracionConfig : IEntityTypeConfiguration<Numeracion>
    {
        public void Configure(EntityTypeBuilder<Numeracion> builder)
        {
            builder.ToTable("gnrNumeraciones");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre).HasMaxLength(50);
            builder.Property(x => x.Letra).HasMaxLength(1);
            builder.Property(x => x.CodigoAutorizacion).HasMaxLength(14);
            builder.Property(x => x.Puerto).HasMaxLength(100);
            builder.Property(x => x.RutaPdf).HasMaxLength(100);
            builder.Property(x => x.Asunto).HasMaxLength(255);
        }
    }
}
