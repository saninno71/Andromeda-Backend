using System;

namespace fyd.backend.Infraestructura.General.Entidades
{
    public class Numeracion
    {
        public Numeracion() { }

        public int Id { get; set; }
        public int? EmpresaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Letra { get; set; } = string.Empty;
        public int? PuntoVenta { get; set; }
        public int? Numero { get; set; }
        public int? NumeradorTipo { get; set; }
        public int? NumeroMenor { get; set; }
        public int? NumeroMayor { get; set; }
        public DateTime? FechaMenor { get; set; }
        public DateTime? FechaMayor { get; set; }
        public int? AvisoNumeros { get; set; }
        public int? AvisoDias { get; set; }
        public DateTime? UltimoAviso { get; set; }
        public string CodigoAutorizacion { get; set; } = string.Empty;
        public bool? CtrlFechaNumeroOk { get; set; }
        public int? Copias { get; set; }
        public int? Items { get; set; }
        public int? TipoAutorizacion { get; set; }
        public bool? WebServicesOk { get; set; }
        public int? WebServiceId { get; set; }
        public int? DestinoImpresion { get; set; }
        public string Puerto { get; set; } = string.Empty;
        public int? Bandeja { get; set; }
        public int? PlantillaId { get; set; }
        public string RutaPdf { get; set; } = string.Empty;
        public int? MailNro { get; set; }
        public string Asunto { get; set; } = string.Empty;
        public int? AutorizacionCot { get; set; }
    }
}
