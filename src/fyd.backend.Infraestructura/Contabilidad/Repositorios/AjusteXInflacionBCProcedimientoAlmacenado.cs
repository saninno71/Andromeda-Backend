using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class AjusteXInflacionBCProcedimientoAlmacenado : IAjusteXInflacionBCProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<AjusteXInflacionBCProcedimientoAlmacenado> _log;

        public AjusteXInflacionBCProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<AjusteXInflacionBCProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbAjusteXInflacionBCDetalleReadModel> ObtenerDetalle(
            int fechaCierre,
            int fechaCierreAnterior,
            bool incluyeAjustesOk,
            string empresaId,
            int articuloId,
            int metodo)
        {
            _log.LoguearInformacion("Ejecutando cstctbAjusteXInflacionBC en modo Detalle.");
            
            _contexto.Database.SetCommandTimeout(120);
            
            var resultado =  _contexto.CstctbAjusteXInflacionBCDetalles
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbAjusteXInflacionBC] 
                    {fechaCierre}, 
                    {fechaCierreAnterior}, 
                    {incluyeAjustesOk}, 
                    {empresaId}, 
                    {articuloId}, 
                    {metodo}, 
                    0") // 0 = DevolverTotalesOk (Falso)
                .AsNoTracking()
                
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbAjusteXInflacionBCTotalesReadModel> ObtenerTotales(
            int fechaCierre,
            int fechaCierreAnterior,
            bool incluyeAjustesOk,
            string empresaId,
            int articuloId,
            int metodo)
        {
            _log.LoguearInformacion("Ejecutando cstctbAjusteXInflacionBC en modo Totales.");

            var resultado = _contexto.CstctbAjusteXInflacionBCTotales
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbAjusteXInflacionBC] 
                    {fechaCierre}, 
                    {fechaCierreAnterior}, 
                    {incluyeAjustesOk}, 
                    {empresaId}, 
                    {articuloId}, 
                    {metodo}, 
                    1") // 1 = DevolverTotalesOk (Verdadero)
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
