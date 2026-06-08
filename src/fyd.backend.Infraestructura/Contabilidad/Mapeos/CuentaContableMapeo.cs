using fyd.backend.Dominio.Contabilidad.Parametros;
using InfraEntidades = fyd.backend.Infraestructura.Contabilidad.Entidades;
using DominioEntidades = fyd.backend.Dominio.Contabilidad.Entidades;

namespace fyd.backend.Infraestructura.Contabilidad.Mapeos
{
    public static class CuentaContableMapeo
    {
        /// <summary>
        /// Convierte una entidad de Infraestructura en una entidad de Dominio.
        /// Usa el método estático Crear() para respetar las invariantes del dominio
        /// y luego asigna el Id ya que no forma parte del constructor.
        /// </summary>
        public static DominioEntidades.CuentaContable ADominio(InfraEntidades.CuentaContable infra)
        {
            var parametros = new ParametrosCuenta(
                infra.Codigo,
                infra.Nombre,
                infra.AsientoOk,
                infra.SubcuentaTipo,
                infra.MonedaTipo,
                infra.AjustaOk,
                infra.EmpresaId,
                infra.CuentaIdMadre
            );

            var dominio = DominioEntidades.CuentaContable.Crear(parametros).Valor!;
            dominio.Id = infra.Id;

            if (infra.Grupos != null && infra.Grupos.Any())
                dominio.AsignarGrupos(infra.Grupos.Select(g => GrupoMapeo.ADominio(g)));

            return dominio;
        }

        /// <summary>
        /// Convierte una entidad de Dominio en una entidad de Infraestructura.
        /// Usa el método estático Crear() de la entidad de Infra para respetar su construcción
        /// y luego asigna el Id.
        /// </summary>
        public static InfraEntidades.CuentaContable AInfraestructura(DominioEntidades.CuentaContable dominio)
        {
            return new InfraEntidades.CuentaContable
            {
                Id = dominio.Id,
                Codigo = dominio.Codigo,
                Nombre = dominio.Nombre,
                AsientoOk = dominio.AsientoOk,
                SubcuentaTipo = dominio.SubcuentaTipo,
                MonedaTipo = dominio.MonedaTipo,
                AjustaOk = dominio.AjustaOk,
                EmpresaId = dominio.EmpresaId,
                CuentaIdMadre = dominio.CuentaIdMadre
            };
        }
    }
}
