using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Ventas.Consultas;
using fyd.backend.Dominio.Compras.Consultas;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.ORM
{
    public class ContextoLectura(DbContextOptions<ContextoLectura> options) : DbContext(options)
    {
        //Contabilidad
        public DbSet<CstctbIndicesReadModel> CstctbIndices { get; set; }
        public DbSet<CstctbCuentasReadModel> CstctbCuentas { get; set; }
        public DbSet<CstctbAsientosReadModel> CstctbAsientos { get; set; }
        public DbSet<CstctbMayoresReadModel> CstctbMayores { get; set; }
        public DbSet<CstctbBalancesReadModel> CstctbBalances { get; set; }
        public DbSet<CstctbAjusteXInflacionBCDetalleReadModel> CstctbAjusteXInflacionBCDetalles { get; set; }
        public DbSet<CstctbAjusteXInflacionBCTotalesReadModel> CstctbAjusteXInflacionBCTotales { get; set; }
        public DbSet<CstctbImpositivoIIBBBsAsRetencionReadModel> CstctbImpositivoIIBBBsAsRetenciones { get; set; }
        public DbSet<CstctbImpositivoIIBBBsAsPercepcionReadModel> CstctbImpositivoIIBBBsAsPercepciones { get; set; }
        public DbSet<CstctbImpositivoIIBBCABARetPercReadModel> CstctbImpositivoIIBBCABARetPercs { get; set; }
        public DbSet<CstctbImpositivoIIBBCABANotaCreditoReadModel> CstctbImpositivoIIBBCABANotasCredito { get; set; }
        public DbSet<CstctbImpositivoIVAVentasDebitoFiscalReadModel> CstctbImpositivoIVAVentasDebitoFiscales { get; set; }
        public DbSet<CstctbImpositivoIVAGrupoReadModel> CstctbImpositivoIVAGrupos { get; set; }
        public DbSet<CstctbImpositivoIVASinCreditoFiscalReadModel> CstctbImpositivoIVASinCreditoFiscales { get; set; }
        public DbSet<CstctbImpositivoIVARestitucionDebitoFiscalReadModel> CstctbImpositivoIVARestitucionDebitoFiscales { get; set; }
        public DbSet<CstctbImpositivoIVARetencionReadModel> CstctbImpositivoIVARetenciones { get; set; }
        public DbSet<CstctbImpositivoIVAPercepcionReadModel> CstctbImpositivoIVAPercepciones { get; set; }
        public DbSet<CstctbImpositivoIVAComprasExteriorReadModel> CstctbImpositivoIVAComprasExteriores { get; set; }
        public DbSet<CstctbImpositivoLIDVentasCBTEReadModel> CstctbImpositivoLIDVentasCBTEs { get; set; }
        public DbSet<CstctbImpositivoLIDVentasAlicuotasReadModel> CstctbImpositivoLIDVentasAlicuotas { get; set; }
        public DbSet<CstctbImpositivoLIDComprasCBTEReadModel> CstctbImpositivoLIDComprasCBTEs { get; set; }
        public DbSet<CstctbImpositivoLIDComprasAlicuotasReadModel> CstctbImpositivoLIDComprasAlicuotas { get; set; }
        public DbSet<CstctbImpositivoLIDComprasImportacionAlicuotasReadModel> CstctbImpositivoLIDComprasImportacionAlicuotas { get; set; }
        public DbSet<CstctbImpositivoSiCoReRetPercReadModel> CstctbImpositivoSiCoReRetPercs { get; set; }
        public DbSet<CstctbImpositivoSiCoReSujetosRetenidosReadModel> CstctbImpositivoSiCoReSujetosRetenidos { get; set; }
        public DbSet<CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel> CstctbImpositivoSiFeReFacturacionJurisdicciones { get; set; }
        public DbSet<CstctbImpositivoSiFeReFacturacionTipoImporteReadModel> CstctbImpositivoSiFeReFacturacionTipoImportes { get; set; }
        public DbSet<CstctbImpositivoSiFeReRetencionReadModel> CstctbImpositivoSiFeReRetenciones { get; set; }
        public DbSet<CstctbImpositivoSiFeRePercepcionReadModel> CstctbImpositivoSiFeRePercepciones { get; set; }
        public DbSet<CstctbImpositivoSiFeRePercepcionAduaneraReadModel> CstctbImpositivoSiFeRePercepcionesAduaneras { get; set; }
        public DbSet<CstctbImpositivoSiFeReRecaudacionBancariaReadModel> CstctbImpositivoSiFeReRecaudacionesBancarias { get; set; }
        public DbSet<CstctbImpositivoSujetosVinculadosCBTEReadModel> CstctbImpositivoSujetosVinculadosCBTEs { get; set; }
        public DbSet<CstctbImpositivoSujetosVinculadosAlicuotasReadModel> CstctbImpositivoSujetosVinculadosAlicuotas { get; set; }
        public DbSet<CstctbImpositivoSujetosVinculadosOperacionesReadModel> CstctbImpositivoSujetosVinculadosOperaciones { get; set; }
        public DbSet<CstctbImpositivoDecreto3685CabeceraReadModel> CstctbImpositivoDecreto3685Cabeceras { get; set; }
        public DbSet<CstctbImpositivoDecreto3685DetalleReadModel> CstctbImpositivoDecreto3685Detalles { get; set; }
        public DbSet<CstctbImpositivoDecreto3685PercepcionReadModel> CstctbImpositivoDecreto3685Percepciones { get; set; }

        //Ventas
        public DbSet<CstvtsCtaCteMovimientosReadModel> CstvtsCtaCteMovimientos { get; set; }
        public DbSet<CstvtsClientesReadModel> CstvtsClientesReadModel { get; set; }

        //Compras
        public DbSet<CstProveedoresReadModel> CstProveedoresReadModel { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<CstvtsCtaCteMovimientosReadModel>()
                .HasNoKey();

            builder
                .Entity<CstvtsClientesReadModel>()
                .HasNoKey();

            builder
                .Entity<CstProveedoresReadModel>()                
                .HasNoKey();

            builder
                .Entity<CstctbIndicesReadModel>()
                .HasNoKey();

            builder
                .Entity<CstctbCuentasReadModel>()
                .HasNoKey();

            builder
                .Entity<CstctbAsientosReadModel>()
                .HasNoKey();

            builder
                .Entity<CstctbMayoresReadModel>()
                .HasNoKey();

            builder
                .Entity<CstctbBalancesReadModel>()
                .HasNoKey();

            builder
                   .Entity<CstctbAjusteXInflacionBCDetalleReadModel>()
                   .HasNoKey();

            builder
                   .Entity<CstctbAjusteXInflacionBCTotalesReadModel>()
                   .HasNoKey();

            builder.Entity<CstctbImpositivoIIBBBsAsRetencionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIIBBBsAsPercepcionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIIBBCABARetPercReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIIBBCABANotaCreditoReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVAVentasDebitoFiscalReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVAGrupoReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVASinCreditoFiscalReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVARestitucionDebitoFiscalReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVARetencionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVAPercepcionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoIVAComprasExteriorReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoLIDVentasCBTEReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoLIDVentasAlicuotasReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoLIDComprasCBTEReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoLIDComprasAlicuotasReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoLIDComprasImportacionAlicuotasReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiCoReRetPercReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiCoReSujetosRetenidosReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiFeReFacturacionTipoImporteReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiFeReRetencionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiFeRePercepcionReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiFeRePercepcionAduaneraReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSiFeReRecaudacionBancariaReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSujetosVinculadosCBTEReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSujetosVinculadosAlicuotasReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoSujetosVinculadosOperacionesReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoDecreto3685CabeceraReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoDecreto3685DetalleReadModel>().HasNoKey();
            builder.Entity<CstctbImpositivoDecreto3685PercepcionReadModel>().HasNoKey();
        }
    }
}
