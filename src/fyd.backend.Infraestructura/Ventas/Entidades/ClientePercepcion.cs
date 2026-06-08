using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsCliPercepciones")]
    public class ClientePercepcion
    {
        public ClientePercepcion() { }

        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int EmpresaId { get; set; }
        public int PercepcionId { get; set; }
        public DateTime? ExencionDesde { get; set; }
        public DateTime? ExencionHasta { get; set; }
    }
}
