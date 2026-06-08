using System.Collections.Generic;
using fyd.backend.Dominio.Compras.Enums;
using fyd.backend.Dominio.Compras.Valores;
using fyd.backend.Dominio.General.Valores;

namespace fyd.backend.Dominio.Compras.Parametros
{
    public class ProveedorParametros
    {
        public int Codigo { get; set; }
        public int AgendaId { get; set; }
        public InfoAgenda? InfoAgenda { get; set; }
        public int ProveedorCcId { get; set; }
        public int IvaCategoriaId { get; set; }
        public int CondicionId { get; set; }
        public int CategoriaId { get; set; }
        public int CalificacionId { get; set; }
        public int? CuentaId { get; set; }
        public int? MonedaId { get; set; }
        public float PorcDescuento1 { get; set; }
        public float PorcDescuento2 { get; set; }
        public OrigenProveedor Origen { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public SituacionProveedor Situacion { get; set; }
        public decimal ImpCredito { get; set; }
        public bool EventualOk { get; set; }
        public bool SujetoVinculadoOk { get; set; }
        public IEnumerable<int> Lineas { get; set; } = new List<int>();
        public IEnumerable<InfoRetencion> Retenciones { get; set; } = new List<InfoRetencion>();
    }
}
