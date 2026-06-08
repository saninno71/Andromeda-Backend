using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsProRetenciones")]
    public class ProveedorRetencion
    {
        public ProveedorRetencion() { }

        [Key]
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public int EmpresaId { get; set; }
        public int RetencionId { get; set; }
        public DateTime? ExencionDesde { get; set; }
        public DateTime? ExencionHasta { get; set; }

        public virtual Proveedor? Proveedor { get; set; }
    }
}
