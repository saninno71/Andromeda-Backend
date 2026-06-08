using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Ventas.Consultas;
using fyd.backend.Dominio.Compras.Consultas;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace fyd.backend.API.OData
{
    public static class ModeloEdm
    {
        public static IEdmModel ObtenerModelo()
        {
            ODataConventionModelBuilder builder = new();

            //Contabilidad
            builder.EntitySet<CstctbIndicesReadModel>("CstctbIndices");
            builder.EntitySet<CstctbCuentasReadModel>("CstctbCuentas");
            builder.EntitySet<CstctbAsientosReadModel>("CstctbAsientos");
            builder.EntitySet<CstctbMayoresReadModel>("CstctbMayores");
            builder.EntitySet<CstctbBalancesReadModel>("CstctbBalances");
            builder.EntitySet<CstctbAjusteXInflacionBCDetalleReadModel>("CstctbAjusteXInflacionBCDetalles");
            builder.EntitySet<CstctbAjusteXInflacionBCTotalesReadModel>("CstctbAjusteXInflacionBCTotales");
            builder.EntitySet<CstctbImpositivoIIBBBsAsRetencionReadModel>("CstctbImpositivoIIBBBsAsRetenciones");
            builder.EntitySet<CstctbImpositivoIIBBBsAsPercepcionReadModel>("CstctbImpositivoIIBBBsAsPercepciones");
            builder.EntitySet<CstctbImpositivoIIBBCABARetPercReadModel>("CstctbImpositivoIIBBCABARetPercs");
            builder.EntitySet<CstctbImpositivoIIBBCABANotaCreditoReadModel>("CstctbImpositivoIIBBCABANotasCredito");
            builder.EntitySet<CstctbImpositivoIVAVentasDebitoFiscalReadModel>("CstctbImpositivoIVAVentasDebitoFiscales");
            builder.EntitySet<CstctbImpositivoIVAGrupoReadModel>("CstctbImpositivoIVAGrupos");
            builder.EntitySet<CstctbImpositivoIVASinCreditoFiscalReadModel>("CstctbImpositivoIVASinCreditoFiscales");
            builder.EntitySet<CstctbImpositivoIVARestitucionDebitoFiscalReadModel>("CstctbImpositivoIVARestitucionDebitoFiscales");
            builder.EntitySet<CstctbImpositivoIVARetencionReadModel>("CstctbImpositivoIVARetenciones");
            builder.EntitySet<CstctbImpositivoIVAPercepcionReadModel>("CstctbImpositivoIVAPercepciones");
            builder.EntitySet<CstctbImpositivoIVAComprasExteriorReadModel>("CstctbImpositivoIVAComprasExteriores");
            builder.EntitySet<CstctbImpositivoLIDVentasCBTEReadModel>("CstctbImpositivoLIDVentasCBTEs");
            builder.EntitySet<CstctbImpositivoLIDVentasAlicuotasReadModel>("CstctbImpositivoLIDVentasAlicuotas");
            builder.EntitySet<CstctbImpositivoLIDComprasCBTEReadModel>("CstctbImpositivoLIDComprasCBTEs");
            builder.EntitySet<CstctbImpositivoLIDComprasAlicuotasReadModel>("CstctbImpositivoLIDComprasAlicuotas");
            builder.EntitySet<CstctbImpositivoLIDComprasImportacionAlicuotasReadModel>("CstctbImpositivoLIDComprasImportacionAlicuotas");
            builder.EntitySet<CstctbImpositivoSiCoReRetPercReadModel>("CstctbImpositivoSiCoReRetPercs");
            builder.EntitySet<CstctbImpositivoSiCoReSujetosRetenidosReadModel>("CstctbImpositivoSiCoReSujetosRetenidos");
            builder.EntitySet<CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel>("CstctbImpositivoSiFeReFacturacionJurisdicciones");
            builder.EntitySet<CstctbImpositivoSiFeReFacturacionTipoImporteReadModel>("CstctbImpositivoSiFeReFacturacionTipoImportes");
            builder.EntitySet<CstctbImpositivoSiFeReRetencionReadModel>("CstctbImpositivoSiFeReRetenciones");
            builder.EntitySet<CstctbImpositivoSiFeRePercepcionReadModel>("CstctbImpositivoSiFeRePercepciones");
            builder.EntitySet<CstctbImpositivoSiFeRePercepcionAduaneraReadModel>("CstctbImpositivoSiFeRePercepcionesAduaneras");
            builder.EntitySet<CstctbImpositivoSiFeReRecaudacionBancariaReadModel>("CstctbImpositivoSiFeReRecaudacionesBancarias");
            builder.EntitySet<CstctbImpositivoSujetosVinculadosCBTEReadModel>("CstctbImpositivoSujetosVinculadosCBTEs");
            builder.EntitySet<CstctbImpositivoSujetosVinculadosAlicuotasReadModel>("CstctbImpositivoSujetosVinculadosAlicuotas");
            builder.EntitySet<CstctbImpositivoSujetosVinculadosOperacionesReadModel>("CstctbImpositivoSujetosVinculadosOperaciones");
            builder.EntitySet<CstctbImpositivoDecreto3685CabeceraReadModel>("CstctbImpositivoDecreto3685Cabeceras");
            builder.EntitySet<CstctbImpositivoDecreto3685DetalleReadModel>("CstctbImpositivoDecreto3685Detalles");
            builder.EntitySet<CstctbImpositivoDecreto3685PercepcionReadModel>("CstctbImpositivoDecreto3685Percepciones");

            //Ventas
            builder.EntitySet<CstvtsCtaCteMovimientosReadModel>("CstvtsCtaCteMovimientos");
            builder.EntitySet<CstvtsClientesReadModel>("CstvtsClientes");
            
            //Compras
            builder.EntitySet<CstProveedoresReadModel>("CstProveedores");
            
            return builder.GetEdmModel();
        }
    }
}
