namespace fyd.backend.Dominio.Ventas.Consultas
{
    public class CstvtsClientesReadModel
    {
        // Identificadores principales
        public int ID { get; set; }
        public int AgendaID { get; set; }
        public int Codigo { get; set; }

        // Agenda / Datos personales
        public string Nombre { get; set; } = string.Empty;
        public string? Nombre2 { get; set; }
        public int? TipoDocID { get; set; }
        public string? DocCodigo { get; set; }
        public string? DocNombre { get; set; }
        public string? NumeroDoc { get; set; }
        public string? Domicilio { get; set; }
        public string? Barrio { get; set; }
        public string? CP { get; set; }

        // Localidad / Provincia / País
        public int? LocalidadID { get; set; }
        public string? LocalidadCodigo { get; set; }
        public string? LocalidadNombre { get; set; }
        public int? ProvinciaID { get; set; }
        public string? ProvinciaCodigo { get; set; }
        public string? ProvinciaNombre { get; set; }
        public int? PaisID { get; set; }
        public string? PaisCodigo { get; set; }
        public string? PaisNombre { get; set; }

        // Teléfonos
        public string? TipoTelefono1 { get; set; }
        public string? Telefono1 { get; set; }
        public string? TipoTelefono2 { get; set; }
        public string? Telefono2 { get; set; }
        public string? TipoTelefono3 { get; set; }
        public string? Telefono3 { get; set; }
        public string? TipoTelefono4 { get; set; }
        public string? Telefono4 { get; set; }
        public string? TipoTelefono5 { get; set; }
        public string? Telefono5 { get; set; }

        // Contacto
        public bool? CorrespondenciaOK { get; set; }
        public string? Atencion { get; set; }
        public string? Web { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? Email3 { get; set; }
        public string? Email4 { get; set; }

        // Variables personalizadas
        public string? Variable1 { get; set; }
        public string? Variable2 { get; set; }
        public string? Variable3 { get; set; }
        public string? Variable4 { get; set; }

        // Domicilio entrega
        public bool? CliDomEntregaOK { get; set; }

        // IVA
        public int IVACategoriaID { get; set; }
        public string? IVACategoriaCodigo { get; set; }
        public string? IVACategoriaNombre { get; set; }
        public float? PorcIVALiberado { get; set; }

        // Vendedor
        public int VendedorID { get; set; }
        public string? VendedorCodigo { get; set; }
        public string? VendedorNombre { get; set; }

        // Cobrador
        public int CobradorID { get; set; }
        public string? CobradorCodigo { get; set; }
        public string? CobradorNombre { get; set; }

        // Condición de pago
        public int CondicionID { get; set; }
        public int? CondicionCodigo { get; set; }
        public string? CondicionNombre { get; set; }

        // Descuentos
        public float? PorcDescuento1 { get; set; }
        public float? PorcDescuento2 { get; set; }
        public float? PorcDescuento3 { get; set; }

        // Categoría
        public int CategoriaID { get; set; }
        public int? CategoriaCodigo { get; set; }
        public string? CategoriaNombre { get; set; }

        // Zona
        public int ZonaID { get; set; }
        public int? ZonaCodigo { get; set; }
        public string? ZonaNombre { get; set; }

        // Calificación
        public int CalificacionID { get; set; }
        public int? CalificacionCodigo { get; set; }
        public string? CalificacionNombre { get; set; }

        // Transporte
        public int TransporteID { get; set; }
        public int? TransporteCodigo { get; set; }
        public string? TransporteNombre { get; set; }

        // Situación
        public int Situacion { get; set; }
        public string? SituacionLey { get; set; }

        // Moneda
        public int MonedaID { get; set; }
        public string? MonedaCodigo { get; set; }
        public string? MonedaNombre { get; set; }

        // IIBB
        public string? IB { get; set; }
        public int? JurisdiccionID { get; set; }
        public string? JurisdiccionCodigo { get; set; }
        public string? JurisdiccionNombre { get; set; }

        // Otros datos
        public string? Observaciones { get; set; }
        public string? Titulo { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public decimal? ImpCredito { get; set; }

        // Cliente cuenta corriente
        public int ClienteCCID { get; set; }
        public int? ClienteCCCodigo { get; set; }
        public string? ClienteCCNombre { get; set; }

        // Lista de precios
        public int? ListaID { get; set; }
        public int? ListaCodigo { get; set; }
        public string? ListaNombre { get; set; }

        // Cuenta contable
        public int? CuentaID { get; set; }
        public string? CuentaCodigo { get; set; }
        public string? CuentaNombre { get; set; }

        // Depósito
        public int? DepositoID { get; set; }
        public string? DepositoCodigo { get; set; }
        public string? DepositoNombre { get; set; }

        // Domicilio de entrega
        public int? DomEntregaID { get; set; }
        public string? Domentrega { get; set; }
        public string? DomEntregaBarrio { get; set; }
        public int? DomEntregaLocalidadID { get; set; }
        public string? DomEntregaLocalidadCodigo { get; set; }
        public string? DomEntregaLocalidadNombre { get; set; }
        public int? DomEntregaProvinciaID { get; set; }
        public string? DomEntregaProvinciaCodigo { get; set; }
        public string? DomEntregaProvinciaNombre { get; set; }
        public int? DomEntregaPaisID { get; set; }
        public string? DomEntregaPaisCodigo { get; set; }
        public string? DomEntregaPaisNombre { get; set; }

        // Empresa agenda
        public int? AgendaEmpresaID { get; set; }
        public int? AgendaEmpresaCodigo { get; set; }
        public string? AgendaEmpresaNombre { get; set; }
        public int? CorrespondenciaID { get; set; }

        // Flags
        public bool EventualOK { get; set; }
        public bool SujetoVinculadoOK { get; set; }

        // Datos adicionales
        public string? Dato01 { get; set; }
        public string? Dato02 { get; set; }
        public string? Dato03 { get; set; }
        public string? Dato04 { get; set; }
        public string? Dato05 { get; set; }
        public string? Dato06 { get; set; }
        public string? Dato07 { get; set; }
        public string? Dato08 { get; set; }
        public string? Dato09 { get; set; }
        public string? Dato10 { get; set; }
    }
}
