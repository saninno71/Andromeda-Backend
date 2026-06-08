using System;

namespace fyd.backend.Dominio.Compras.Valores
{
    public record InfoRetencion(
        int RetencionId,
        int EmpresaId,
        DateTime? ExencionDesde,
        DateTime? ExencionHasta
    );
}
