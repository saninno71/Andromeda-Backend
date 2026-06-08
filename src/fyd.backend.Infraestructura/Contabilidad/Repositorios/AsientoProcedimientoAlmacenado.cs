using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class AsientoProcedimientoAlmacenado : IAsientoProcedimientoAlmacenado
    {

        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<AsientoProcedimientoAlmacenado> _log;

        public AsientoProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<AsientoProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbAsientosReadModel> ObtenerCstctbAsientos(int? id, int? fechaDesde, int? fechaHasta, string? detalle, int? cuentaId, int? numeroDesde, int? numeroHasta, int? subcuentaClienteId, int? subcuentaProveedorId, int? subcuentaCajaId, string? empresaId, int? numeraTipoId)
        {
            var resultado = _contexto.CstctbAsientos
                    .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbAsientos] 
                    {id}, 
                    {fechaDesde}, 
                    {fechaHasta}, 
                    {detalle}, 
                    {cuentaId}, 
                    {numeroDesde}, 
                    {numeroHasta},
                    {subcuentaClienteId},
                    {subcuentaProveedorId},
                    {subcuentaCajaId},
                    {empresaId},
                    {numeraTipoId}") 
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
