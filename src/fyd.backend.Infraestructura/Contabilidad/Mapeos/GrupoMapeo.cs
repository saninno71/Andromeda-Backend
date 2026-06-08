using InfraEntidades = fyd.backend.Infraestructura.Contabilidad.Entidades;
using DominioEntidades = fyd.backend.Dominio.Contabilidad.Entidades;

namespace fyd.backend.Infraestructura.Contabilidad.Mapeos
{
    public static class GrupoMapeo
    {
        public static DominioEntidades.Grupo ADominio(InfraEntidades.Grupo infra)
        {
            var dominio = DominioEntidades.Grupo.Crear(infra.Codigo, infra.Nombre).Valor!;
            dominio.Id = infra.Id;
            return dominio;
        }

        public static InfraEntidades.Grupo AInfraestructura(DominioEntidades.Grupo dominio)
        {
            return new InfraEntidades.Grupo
            {
                Id = dominio.Id,
                Codigo = dominio.Codigo,
                Nombre = dominio.Nombre
            };
        }
    }
}
