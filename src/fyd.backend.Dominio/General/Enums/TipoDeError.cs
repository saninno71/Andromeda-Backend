using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.General.Enums
{
    public enum TipoDeError
    {
        Validacion, //Falla en una regla de negocio (400)
        NoEncontrado, //404
        Conflicto, //409
    }
}
