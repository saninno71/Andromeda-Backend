using fyd.backend.Dominio.Compras.Entidades;
using fyd.backend.Dominio.Compras.Repositorios;
using fyd.backend.Infraestructura.Compras.Mapeos;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Compras.Repositorios
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly ContextoAplicacion _contexto;

        public ProveedorRepositorio(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task Agregar(Proveedor entidad)
        {
            var infra = ProveedorMapeo.AInfraestructura(entidad);
            await _contexto.Proveedores.AddAsync(infra);
            _contexto.SavedChanges += (_, _) => entidad.Id = infra.Id;
        }

        public void Actualizar(Proveedor entidad)
        {
            var infra = _contexto.Proveedores
                .Include(p => p.Agenda)
                .Include(p => p.Lineas)
                .Include(p => p.Retenciones)
                .First(p => p.Id == entidad.Id);

            // Mapeo simple
            infra.Codigo = entidad.Codigo;
            infra.AgendaId = entidad.AgendaId;
            infra.ProveedorCcId = entidad.ProveedorCcId;
            infra.IvaCategoriaId = entidad.IvaCategoriaId;
            infra.CondicionId = entidad.CondicionId;
            infra.CategoriaId = entidad.CategoriaId;
            infra.CalificacionId = entidad.CalificacionId;
            infra.CuentaId = entidad.CuentaId;
            infra.MonedaId = entidad.MonedaId;
            infra.PorcDescuento1 = entidad.PorcDescuento1;
            infra.PorcDescuento2 = entidad.PorcDescuento2;
            infra.Origen = (int)entidad.Origen;
            infra.FechaAlta = entidad.FechaAlta;
            infra.FechaBaja = entidad.FechaBaja;
            infra.Situacion = (int)entidad.Situacion;
            infra.ImpCredito = entidad.ImpCredito;
            infra.EventualOk = entidad.EventualOk;
            infra.SujetoVinculadoOk = entidad.SujetoVinculadoOk;

            // Mapeo M-M (Colecciones)
            infra.Lineas.Clear();
            if (entidad.Lineas != null)
            {
                foreach (var l in entidad.Lineas)
                    infra.Lineas.Add(new Entidades.ProveedorLinea { LineaId = l });
            }

            infra.Retenciones.Clear();
            if (entidad.Retenciones != null)
            {
                foreach (var r in entidad.Retenciones)
                    infra.Retenciones.Add(new Entidades.ProveedorRetencion 
                    { 
                        RetencionId = r.RetencionId, 
                        EmpresaId = r.EmpresaId, 
                        ExencionDesde = r.ExencionDesde, 
                        ExencionHasta = r.ExencionHasta 
                    });
            }

            // Propagación sobre Agenda (Aggregate)
            if (entidad.InfoAgenda != null)
            {
                if (infra.Agenda == null) infra.Agenda = new General.Entidades.Agenda();
                infra.Agenda.EmpresaId = entidad.InfoAgenda.EmpresaId;
                infra.Agenda.Nombre = entidad.InfoAgenda.Nombre;
                infra.Agenda.Nombre2 = entidad.InfoAgenda.Nombre2;
                infra.Agenda.Titulo = entidad.InfoAgenda.Titulo;
                infra.Agenda.Calle = entidad.InfoAgenda.Calle;
                infra.Agenda.Altura = entidad.InfoAgenda.Altura;
                infra.Agenda.PisoDpto = entidad.InfoAgenda.PisoDpto;
                infra.Agenda.Barrio = entidad.InfoAgenda.Barrio;
                infra.Agenda.LocalidadId = entidad.InfoAgenda.LocalidadId;
                infra.Agenda.Cp = entidad.InfoAgenda.Cp;
                infra.Agenda.TipoDocId = entidad.InfoAgenda.TipoDocId;
                infra.Agenda.NumeroDoc = entidad.InfoAgenda.NumeroDoc;
                infra.Agenda.Ib = entidad.InfoAgenda.Ib;
                infra.Agenda.Email1 = entidad.InfoAgenda.Email1;
                infra.Agenda.Email2 = entidad.InfoAgenda.Email2;
                infra.Agenda.Email3 = entidad.InfoAgenda.Email3;
                infra.Agenda.Email4 = entidad.InfoAgenda.Email4;
                infra.Agenda.Web = entidad.InfoAgenda.Web;
                infra.Agenda.TipoTelefono1 = entidad.InfoAgenda.TipoTelefono1;
                infra.Agenda.Telefono1 = entidad.InfoAgenda.Telefono1;
                infra.Agenda.TipoTelefono2 = entidad.InfoAgenda.TipoTelefono2;
                infra.Agenda.Telefono2 = entidad.InfoAgenda.Telefono2;
                infra.Agenda.TipoTelefono3 = entidad.InfoAgenda.TipoTelefono3;
                infra.Agenda.Telefono3 = entidad.InfoAgenda.Telefono3;
                infra.Agenda.TipoTelefono4 = entidad.InfoAgenda.TipoTelefono4;
                infra.Agenda.Telefono4 = entidad.InfoAgenda.Telefono4;
                infra.Agenda.TipoTelefono5 = entidad.InfoAgenda.TipoTelefono5;
                infra.Agenda.Telefono5 = entidad.InfoAgenda.Telefono5;
                infra.Agenda.Variable1 = entidad.InfoAgenda.Variable1;
                infra.Agenda.Variable2 = entidad.InfoAgenda.Variable2;
                infra.Agenda.Variable3 = entidad.InfoAgenda.Variable3;
                infra.Agenda.Variable4 = entidad.InfoAgenda.Variable4;
                infra.Agenda.CorrespondenciaOk = entidad.InfoAgenda.CorrespondenciaOk;
                infra.Agenda.CorrespondenciaId = entidad.InfoAgenda.CorrespondenciaId;
                infra.Agenda.Atencion = entidad.InfoAgenda.Atencion;
                infra.Agenda.CliDomEntregaOk = entidad.InfoAgenda.CliDomEntregaOk;
                infra.Agenda.Memo = entidad.InfoAgenda.Memo;
            }
        }

        public void Eliminar(Proveedor entidad)
        {
            _contexto.Proveedores.Remove(ProveedorMapeo.AInfraestructura(entidad));
        }

        public async Task<Proveedor?> ObtenerPorId(int id)
        {
            var infra = await _contexto.Proveedores
                .Include(p => p.Agenda)
                .Include(p => p.Lineas)
                .Include(p => p.Retenciones)
                .FirstOrDefaultAsync(p => p.Id == id);

            return infra is null ? null : ProveedorMapeo.ADominio(infra);
        }

        public async Task<bool> ExisteCodigo(int codigo)
        {
            return await _contexto.Proveedores.AnyAsync(p => p.Codigo == codigo);
        }

        public async Task<int> ObtenerMaximoCodigo()
        {
            if (!await _contexto.Proveedores.AnyAsync()) return 0;
            return await _contexto.Proveedores.MaxAsync(p => p.Codigo);
        }

        public async Task<bool> EnUso(int id)
        {
            // Verifica en entidades vinculadas como Memos o Artículos
            bool enUso = await _contexto.ProveedoresArticulos.AnyAsync(p => p.ProveedorId == id) ||
                         await _contexto.CompraComprobantes.AnyAsync(c => c.ProveedorId == id);
            return enUso;
        }

        public async Task<bool> TieneMovimientosEnCc(int id)
        {
            return await _contexto.CompraComprobantes.AnyAsync(c => c.ProveedorCcId == id);
        }
    }
}
