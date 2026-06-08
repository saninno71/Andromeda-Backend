using fyd.backend.Infraestructura.General.Entidades;
using System;
using fyd.backend.Infraestructura.Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsProveedores")]
    public class Proveedor : IEntityTypeConfiguration<Proveedor>
    {
        public Proveedor() { }

        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int AgendaId { get; set; }
        public int ProveedorCcId { get; set; }
        public int IvaCategoriaId { get; set; }
        public int CondicionId { get; set; }
        public int CategoriaId { get; set; }
        public int CalificacionId { get; set; }
        public int? CuentaId { get; set; }
        public int? MonedaId { get; set; }
        public float PorcDescuento1 { get; set; }
        public float PorcDescuento2 { get; set; }
        public int Origen { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int Situacion { get; set; }
        
        [Precision(18, 4)]
        public decimal ImpCredito { get; set; }
        
        public bool EventualOk { get; set; }
        public bool SujetoVinculadoOk { get; set; }

        [ForeignKey(nameof(AgendaId))]
        public virtual Agenda? Agenda { get; set; }
        
        [ForeignKey(nameof(CuentaId))]
        public virtual CuentaContable? Cuenta { get; set; }
        
        [ForeignKey(nameof(ProveedorCcId))]
        public virtual Proveedor? ProveedorCc { get; set; }

        public virtual ICollection<ProveedorArticulo> Articulos { get; set; } = new List<ProveedorArticulo>();
        public virtual ICollection<ProveedorLinea> Lineas { get; set; } = new List<ProveedorLinea>();
        public virtual ICollection<ProveedorMemo> Memos { get; set; } = new List<ProveedorMemo>();
        public virtual ICollection<ProveedorRetencion> Retenciones { get; set; } = new List<ProveedorRetencion>();

        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.HasOne(p => p.Agenda).WithMany().HasForeignKey(p => p.AgendaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Cuenta).WithMany().HasForeignKey(p => p.CuentaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.ProveedorCc).WithMany().HasForeignKey(p => p.ProveedorCcId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Articulos).WithOne(a => a.Proveedor).HasForeignKey(a => a.ProveedorId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Lineas).WithOne(l => l.Proveedor).HasForeignKey(l => l.ProveedorId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Memos).WithOne(m => m.Proveedor).HasForeignKey(m => m.ProveedorId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Retenciones).WithOne(r => r.Proveedor).HasForeignKey(r => r.ProveedorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
