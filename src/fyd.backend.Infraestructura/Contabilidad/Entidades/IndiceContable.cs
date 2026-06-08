using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Contabilidad.Entidades
{
    [Table("ctbIndices")]
    public class IndiceContable : IEntityTypeConfiguration<IndiceContable>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Periodo { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal Indice { get; set; }

        public IndiceContable() { }

        public void Configure(EntityTypeBuilder<IndiceContable> builder)
        {
            builder.Property(e => e.Indice)
                   .HasPrecision(18, 4);
        }
    }
}
