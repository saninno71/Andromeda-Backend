using fyd.backend.Dominio.Contabilidad.Consultas;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IAjusteXInflacionBCProcedimientoAlmacenado
    {
        IQueryable<CstctbAjusteXInflacionBCDetalleReadModel> ObtenerDetalle(int fechaCierre,int fechaCierreAnterior,bool incluyeAjusteOK,string empresaID, int articuloId, int metodo);
        IQueryable<CstctbAjusteXInflacionBCTotalesReadModel> ObtenerTotales(int fechaCierre, int fechaCierreAnterior, bool incluyeAjusteOK, string empresaID, int articuloId, int metodo);
    }
}
