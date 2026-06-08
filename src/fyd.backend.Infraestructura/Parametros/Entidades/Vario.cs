using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Parametros.Entidades
{
    [Table("prmVarios")]
    public class Vario
    {
        public Vario() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Codigo { get; set; }

        public int? Tipo { get; set; }
        public int? Longitud { get; set; }
        public bool? ArrayOk { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        private readonly List<VarioEmpresa> _empresas = new();
        public IReadOnlyCollection<VarioEmpresa> Empresas => _empresas.AsReadOnly();
    }
}
