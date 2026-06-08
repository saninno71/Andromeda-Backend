using System;

namespace fyd.backend.Dominio.Compras.Consultas
{
    public class CstProveedoresReadModel
    {
        public int ID { get; set; }
        public int AgendaID { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Nombre2 { get; set; }
        public string? Titulo { get; set; }
        public int IVACategoriaID { get; set; }
        public string? IVACategoriaCodigo { get; set; }
        public string? IVACategoriaNombre { get; set; }
        public int TipoDocID { get; set; }
        public string? DocCodigo { get; set; }
        public string? DocNombre { get; set; }
        public string? NumeroDoc { get; set; }
        public string? IB { get; set; }
        public string? Domicilio { get; set; }
        public string? Barrio { get; set; }
        public string? CP { get; set; }
        public int LocalidadID { get; set; }
        public string? LocalidadCodigo { get; set; }
        public string? LocalidadNombre { get; set; }
        public int ProvinciaID { get; set; }
        public string? ProvinciaCodigo { get; set; }
        public string? ProvinciaNombre { get; set; }
        public int PaisID { get; set; }
        public string? PaisCodigo { get; set; }
        public string? PaisNombre { get; set; }
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
        public int CategoriaID { get; set; }
        public int? CategoriaCodigo { get; set; }
        public string? CategoriaNombre { get; set; }
        public int CalificacionID { get; set; }
        public int? CalificacionCodigo { get; set; }
        public string? CalificacionNombre { get; set; }
        public int Situacion { get; set; }
        public string? SituacionLey { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int? CuentaID { get; set; }
        public string? CuentaCodigo { get; set; }
        public string? CuentaNombre { get; set; }
        public int CondicionID { get; set; }
        public int? CondicionCodigo { get; set; }
        public string? CondicionNombre { get; set; }
        public int ProveedorCCID { get; set; }
        public int? ProveedorCCCodigo { get; set; }
        public string? ProveedorCCNombre { get; set; }
        public float PorcDescuento1 { get; set; }
        public float PorcDescuento2 { get; set; }
        public int Origen { get; set; }
        public string? OrigenLey { get; set; }
        public decimal ImpCredito { get; set; }
        public int? MonedaID { get; set; }
        public string? MonedaCodigo { get; set; }
        public string? MonedaNombre { get; set; }
        public bool CorrespondenciaOK { get; set; }
        public int CorrespondenciaID { get; set; }
        public string? Atencion { get; set; }
        public string? Web { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? Email3 { get; set; }
        public string? Email4 { get; set; }
        public string? Variable1 { get; set; }
        public string? Variable2 { get; set; }
        public string? Variable3 { get; set; }
        public string? Variable4 { get; set; }
        public int? AgendaEmpresaID { get; set; }
        public int? AgendaEmpresaCodigo { get; set; }
        public string? AgendaEmpresaNombre { get; set; }
        public string? Observaciones { get; set; }
        public int? RetencionEmpresaCodigo { get; set; }
        public string? RetencionEmpresaNombre { get; set; }
        public bool EventualOK { get; set; }
        public bool SujetoVinculadoOK { get; set; }
        public int? IVAGrupoID { get; set; }
        public int? IVAGrupoCodigo { get; set; }
        public string? IVAGrupoNombre { get; set; }
    }
}
