namespace fyd.backend.API.ParametrosDeConsulta.Ventas
{
    public class CstvtsClientesParams
    {
        public int? Id { get; set; }
        public int? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? NumeroDoc { get; set; }
        public string? Calle { get; set; }
        public string? Barrio { get; set; }
        public int? LocalidadID { get; set; }
        public int? ProvinciaID { get; set; }
        public int? PaisID { get; set; }
        public int? IVACategoriaID { get; set; }
        public int? ClienteCCID { get; set; }
        public int? VendedorID { get; set; }
        public int? CobradorID { get; set; }
        public int? CondPagoID { get; set; }
        public int? ListaPrecioID { get; set; }
        public int? CuentaID { get; set; }
        public int? TransporteID { get; set; }
        public int? DepositoID { get; set; }
        public int? Situacion { get; set; }
        public int? CategoriaID { get; set; }
        public int? FechaAltaDesde { get; set; }
        public int? FechaBajaDesde { get; set; }
        public int? FechaAltaHasta { get; set; }
        public int? FechaBajaHasta { get; set; }
        public int? ZonaID { get; set; }
        public int? LineaID { get; set; }
        public int? CalificacionID { get; set; }
        public string? Telefono { get; set; }
        public string? Variables { get; set; }
        public string? Lineas { get; set; }
        public string? AgendaEmpresaID { get; set; }
        public string? CUIT { get; set; }
        public int? ClienteEventual { get; set; }
        public bool ConsultaCompletaOK { get; set; } = true; // Si esto es false, va a tener que funcionar como otro endpoint
    }
}
