using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Errores
{
    public class CuentaContableCodigoError
    {
        public const string NoEncontrada = "CuentaContable.NoEncontrada";
        public const string CodigoVacio = "CuentaContable.CodigoVacio";
        public const string NombreVacio = "CuentaContable.NombreVacio";
        public const string CodigoDuplicado = "CuentaContable.CodigoDuplicado";
        public const string CuentaMadreNoValida = "CuentaContable.CuentaMadreNoValida";
        public const string ReferenciaCircular = "CuentaContable.ReferenciaCircular";
        public const string TieneMovimientos = "CuentaContable.TieneMovimientos";
        public const string TieneCuentasHijas = "CuentaContable.TieneCuentasHijas";
        public const string SuperaNivelMaximoPermitido = "CuentaContable.SuperaNivelMaximoPermitido";
        public const string GrupoNoEncontrado = "CuentaContable.GrupoNoEncontrado";
    }
}
