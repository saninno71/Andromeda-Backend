using DominioEntidades = fyd.backend.Dominio.Contabilidad.Entidades;
using InfraEntidades = fyd.backend.Infraestructura.Contabilidad.Entidades;

namespace fyd.backend.Infraestructura.Contabilidad.Mapeos
{
    public static class AsientoMapeo
    {
        public static DominioEntidades.Asiento ADominio(InfraEntidades.Asiento infra)
        {
            var dominio = DominioEntidades.Asiento.Crear(
                infra.ComprobanteId,
                infra.CuentaId,
                infra.Tipo,
                infra.Importe,
                infra.ImpLocal,
                infra.ImpReferencia,
                infra.Detalle,
                infra.ClienteId,
                infra.ProveedorId,
                infra.CajaId).Valor!;

            dominio.Id = infra.Id;
            return dominio;
        }

        public static InfraEntidades.Asiento AInfraestructura(DominioEntidades.Asiento dominio)
        {
            return new InfraEntidades.Asiento
            {
                Id = dominio.Id,
                ComprobanteId = dominio.ComprobanteId,
                CuentaId = dominio.CuentaId,
                Tipo = dominio.Tipo,
                Importe = dominio.Importe,
                ImpLocal = dominio.ImpLocal,
                ImpReferencia = dominio.ImpReferencia,
                Detalle = dominio.Detalle,
                ClienteId = dominio.ClienteId,
                ProveedorId = dominio.ProveedorId,
                CajaId = dominio.CajaId,
                EliminarOk = dominio.EliminarOk
            };
        }
    }
}
