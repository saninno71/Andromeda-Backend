using fyd.backend.Dominio.Contabilidad.Parametros;
using InfraEntidades = fyd.backend.Infraestructura.Contabilidad.Entidades;
using DominioEntidades = fyd.backend.Dominio.Contabilidad.Entidades;

namespace fyd.backend.Infraestructura.Contabilidad.Mapeos
{
    public static class IndiceContableMapeo
    {
        public static DominioEntidades.IndiceContable ADominio(InfraEntidades.IndiceContable infra)
        {
            var parametros = new ParametrosIndice(infra.Periodo, infra.Indice);
            var dominio = DominioEntidades.IndiceContable.Crear(parametros).Valor!;
            dominio.Id = infra.Id;
            return dominio;
        }

        public static InfraEntidades.IndiceContable AInfraestructura(DominioEntidades.IndiceContable dominio)
        {
            return new InfraEntidades.IndiceContable
            {
                Id = dominio.Id,
                Periodo = dominio.Periodo,
                Indice = dominio.Indice
            };
        }
    }
}
