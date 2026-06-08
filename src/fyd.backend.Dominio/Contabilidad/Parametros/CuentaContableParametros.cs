using fyd.backend.Dominio.Contabilidad.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Parametros
{
    public record ParametrosCuenta(
            string Codigo,
            string Nombre,
            bool AsientoOk,
            SubcuentaTipo SubcuentaTipo,
            MonedaTipo MonedaTipo,
            bool AjustaOk,
            int? EmpresaId,
            int? CuentaIdMadre);   
}
