using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Ventas.Errores
{
    public class ClienteCodigoError
    {
        public const string NoEncontrado = "Cliente.NoEncontrado";
        public const string CodigoInvalido = "Cliente.CodigoInvalido";
        public const string RazonSocialVacia = "Cliente.RazonSocialVacia";
        public const string CampoObligatorioFaltante = "Cliente.CampoObligatorioFaltante";
        public const string CuitInvalido = "Cliente.CuitInvalido";

        // Reglas de negocio: Consolidación de Cuenta Corriente
        public const string ConsolidacionEmpresaIncompatible = "Cliente.ConsolidacionEmpresaIncompatible";
        public const string ConsolidacionCircular = "Cliente.ConsolidacionCircular";

        // Reglas de negocio: Empresa
        public const string EmpresaIdNoNulo = "Cliente.EmpresaIdNoNulo";

        // Reglas de negocio: Eliminación
        public const string EsClientePredeterminado = "Cliente.EsClientePredeterminado";
        public const string EsEventualPredeterminado = "Cliente.EsEventualPredeterminado";
        public const string TieneComprobantes = "Cliente.TieneComprobantes";
        public const string TieneContactos = "Cliente.TieneContactos";
        public const string TieneMemos = "Cliente.TieneMemos";
        public const string ActuaComoClienteCC = "Cliente.ActuaComoClienteCC";
        public const string FechaAltaPosteriorABaja = "Cliente.FechaAltaPosteriorABaja";
        public const string SituacionInvalida = "Cliente.SituacionInvalida";
        public const string DescuentoNegativo = "Cliente.DescuentoNegativo";
        public const string PorcentajeIvaLiberadoInvalido = "Cliente.PorcentajeIvaLiberadoInvalido";
        public const string CreditoNegativo = "Cliente.CreditoNegativo";
        public const string CodigoDuplicado = "Cliente.CodigoDuplicado";
        public const string TipoDocumentoIncompatible = "Cliente.TipoDocumentoIncompatible";
        public const string ImpuestoNoEsPercepcion = "Cliente.ImpuestoNoEsPercepcion";
    }
}
