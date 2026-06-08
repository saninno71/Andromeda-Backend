using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Ventas.Enums
{
    public enum SituacionCliente
    {
        ActivoNormal = 1,
        ActivoDeRiesgo = 2,
        EnGestionJudicial = 3,
        Incobrable = 4,
        EnConvocatoriaDeAcreedores = 5,
        EnQuiebra = 6,
        Inactivo = 7
    }
}
