using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Fondos.Entidades
{
    [Table("fdsCajas")]
    public class Caja
    {
        public Caja() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(5)]
        public string Codigo { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        public int? EmpresaId { get; set; }
        public int? BancoId { get; set; }
        public int? MonedaId { get; set; }
        public int? CuentaId { get; set; }
        public int? ProveedorId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ImpDescubierto { get; set; }

        [Column(TypeName = "money")]
        public decimal? ImpMaxCaucion { get; set; }

        public int? TipoCuenta { get; set; }

        [MaxLength(22)]
        public string Cbu { get; set; }
    }
}
