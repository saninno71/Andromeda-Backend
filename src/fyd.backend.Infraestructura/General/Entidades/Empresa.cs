using fyd.backend.Infraestructura.Parametros.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrEmpresas")]
    public class Empresa
    {
        public Empresa() { }

        [Key]
        public int Id { get; set; }
        public int? Codigo { get; set; }
        public int? AgendaId { get; set; }
        public int? IvaCategoriaId { get; set; }

        [MaxLength(15)]
        public string ClaveArba { get; set; }

        private readonly List<VarioEmpresa> _varios = new();
        public IReadOnlyCollection<VarioEmpresa> Varios => _varios.AsReadOnly();
    }
}
