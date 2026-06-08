namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbIndicesReadModel
    {
        public int Id { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal Indice { get; set; }
    }
}
