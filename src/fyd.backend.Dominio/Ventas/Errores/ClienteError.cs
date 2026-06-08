using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Ventas.Errores
{
    public class ClienteError
    {
        public static Error NoEncontrado(int id) => new(
            ClienteCodigoError.NoEncontrado,
            $"El cliente {id} solicitado no existe.",
            TipoDeError.NoEncontrado);

        public static Error CodigoInvalido(string nombre) => new(
            ClienteCodigoError.CodigoInvalido,
            $"El cliente {nombre} no se pudo guardar en la base de datos porque su código es 0 (cero).",
            TipoDeError.Validacion);

        public static Error RazonSocialVacia => new(
            ClienteCodigoError.RazonSocialVacia,
            "La razón social del cliente es obligatoria.",
            TipoDeError.Validacion);

        public static Error CampoObligatorioFaltante(string campo) => new(
            ClienteCodigoError.CampoObligatorioFaltante,
            $"El campo '{campo}' es obligatorio y no puede quedar vacío.",
            TipoDeError.Validacion);

        public static Error CuitInvalido => new(
            ClienteCodigoError.CuitInvalido,
            "El campo CUIT debe ser un número de 11 dígitos corridos, sin guiones ni puntos separadores.",
            TipoDeError.Validacion);

        public static Error ConsolidacionEmpresaIncompatible(string mensaje) => new(
            ClienteCodigoError.ConsolidacionEmpresaIncompatible,
            mensaje,
            TipoDeError.Conflicto);

        public static Error ConsolidacionCircular => new(
            ClienteCodigoError.ConsolidacionCircular,
            "Un cliente que es usado para consolidar la cuenta corriente de otro cliente, no puede consolidar su propia cuenta corriente en un tercero.",
            TipoDeError.Conflicto);

        public static Error EmpresaIdNoNulo => new(
            ClienteCodigoError.EmpresaIdNoNulo,
            "Si el cliente es además empresa, el identificador de empresa debe ser nulo.",
            TipoDeError.Validacion);

        public static Error EsClientePredeterminado => new(
            ClienteCodigoError.EsClientePredeterminado,
            "El cliente está definido como predeterminado y no podrá eliminarse hasta que se modifique o elimine el parámetro correspondiente.",
            TipoDeError.Conflicto);

        public static Error EsEventualPredeterminado => new(
            ClienteCodigoError.EsEventualPredeterminado,
            "El cliente está definido como eventual predeterminado y no podrá eliminarse hasta que se modifique o elimine el parámetro correspondiente.",
            TipoDeError.Conflicto);

        public static Error TieneComprobantes => new(
            ClienteCodigoError.TieneComprobantes,
            "El cliente no puede eliminarse porque está siendo usado en comprobantes.",
            TipoDeError.Conflicto);

        public static Error TieneContactos => new(
            ClienteCodigoError.TieneContactos,
            "El cliente no puede eliminarse porque tiene contactos asociados en la agenda.",
            TipoDeError.Conflicto);

        public static Error TieneMemos => new(
            ClienteCodigoError.TieneMemos,
            "El cliente no puede eliminarse porque tiene memos asociados.",
            TipoDeError.Conflicto);

        public static Error ActuaComoClienteCC => new(
            ClienteCodigoError.ActuaComoClienteCC,
            "El cliente no puede eliminarse porque actúa como consolidador de cuenta corriente para otros clientes.",
            TipoDeError.Conflicto);

        public static Error FechaAltaPosteriorABaja => new(
            ClienteCodigoError.FechaAltaPosteriorABaja,
            "La fecha de alta no puede ser posterior a la fecha de baja.",
            TipoDeError.Validacion);

        public static Error SituacionInvalida => new(
            ClienteCodigoError.SituacionInvalida,
            "La situación del cliente no es válida.",
            TipoDeError.Validacion);

        public static Error DescuentoNegativo => new(
            ClienteCodigoError.DescuentoNegativo,
            "Los porcentajes de descuento no pueden ser negativos.",
            TipoDeError.Validacion);

        public static Error PorcentajeIvaLiberadoInvalido => new(
            ClienteCodigoError.PorcentajeIvaLiberadoInvalido,
            "El porcentaje de IVA liberado debe estar comprendido entre 0 y 100.",
            TipoDeError.Validacion);

        public static Error CreditoNegativo => new(
            ClienteCodigoError.CreditoNegativo,
            "El crédito otorgado no puede ser negativo.",
            TipoDeError.Validacion);

        public static Error CodigoDuplicado => new(
            ClienteCodigoError.CodigoDuplicado,
            "El código de cliente ingresado ya existe en la base de datos.",
            TipoDeError.Conflicto);

        public static Error TipoDocumentoIncompatible => new(
            ClienteCodigoError.TipoDocumentoIncompatible,
            "El tipo de documento seleccionado es incompatible con la categoría de IVA del cliente.",
            TipoDeError.Validacion);

        public static Error ImpuestoNoEsPercepcion => new(
            ClienteCodigoError.ImpuestoNoEsPercepcion,
            "El impuesto seleccionado no corresponde a una percepción.",
            TipoDeError.Validacion);
    }
}
