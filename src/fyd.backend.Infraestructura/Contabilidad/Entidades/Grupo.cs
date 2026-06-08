using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Contabilidad.Entidades
{
    [Table("ctbGrupos")]
    public class Grupo : IEntityTypeConfiguration<Grupo>
    {
        public Grupo() { }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<CuentaContable> CuentasContables { get; set; } = new List<CuentaContable>();

        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            // UsingEntity con shadow entity y FK names custom no tiene equivalente en annotations
            builder.HasMany(g => g.CuentasContables)
                   .WithMany(c => c.Grupos)
                   .UsingEntity<Dictionary<string, object>>(
                       "ctbGruposCuentas",
                       right => right.HasOne<CuentaContable>().WithMany().HasForeignKey("CuentaID"),
                       left => left.HasOne<Grupo>().WithMany().HasForeignKey("GrupoID"),
                       join => join.ToTable("ctbGruposCuentas")
                   );
        }
    }
}
