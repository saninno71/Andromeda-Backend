using fyd.backend.Infraestructura.General.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Parametros.Entidades
{
    [Table("prmVariosEmpresa")]
    [PrimaryKey(nameof(VarioId), nameof(EmpresaId))]
    public class VarioEmpresa : IEntityTypeConfiguration<VarioEmpresa>
    {
        public VarioEmpresa() { }

        public int VarioId { get; set; }
        public int EmpresaId { get; set; }

        [MaxLength(255)]
        public string Valor { get; set; }

        public void Configure(EntityTypeBuilder<VarioEmpresa> builder)
        {
            // Relaciones sin navigation property en VarioEmpresa: requieren Fluent API
            builder.HasOne<Vario>()
                   .WithMany(v => v.Empresas)
                   .HasForeignKey(ve => ve.VarioId);

            builder.HasOne<Empresa>()
                   .WithMany(e => e.Varios)
                   .HasForeignKey(ve => ve.EmpresaId);
        }
    }
}
