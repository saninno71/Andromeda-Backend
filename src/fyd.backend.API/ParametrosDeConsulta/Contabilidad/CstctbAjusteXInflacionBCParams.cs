namespace fyd.backend.API.ParametrosDeConsulta.Contabilidad
{
    public class CstctbAjusteXInflacionBCParams
    {
        public int FechaCierre { get; set; }
        public int FechaCierreAnterior { get; set; }
        public bool IncluyeAjustesOK { get; set; }
        public string EmpresaID { get; set; } = string.Empty;
        public int ArticuloID { get; set; }
        public int Metodo { get; set; }
    }
}
