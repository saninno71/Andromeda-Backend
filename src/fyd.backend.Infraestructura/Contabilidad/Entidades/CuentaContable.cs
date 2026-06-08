using fyd.backend.Dominio.Contabilidad.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Contabilidad.Entidades
{
    [Table("ctbCuentas")]
    public class CuentaContable : IEntityTypeConfiguration<CuentaContable>
    {
        #region propiedades
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(12)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public int? EmpresaId { get; set; }
        public int? CuentaIdMadre { get; set; }
        public bool AsientoOk { get; set; }
        public bool AjustaOk { get; set; }
        public SubcuentaTipo SubcuentaTipo { get; set; }
        public MonedaTipo MonedaTipo { get; set; }

        public virtual CuentaContable? CuentaMadre { get; set; }
        public virtual ICollection<CuentaContable> CuentasHijas { get; set; } = new List<CuentaContable>();
        public virtual ICollection<Grupo> Grupos { get; set; }
        #endregion

        public CuentaContable() { }

        public void Configure(EntityTypeBuilder<CuentaContable> builder)
        {
            // DeleteBehavior.Restrict no tiene equivalente en annotations
            builder.HasOne(hijo => hijo.CuentaMadre)
                   .WithMany(padre => padre.CuentasHijas)
                   .HasForeignKey(hijo => hijo.CuentaIdMadre)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
