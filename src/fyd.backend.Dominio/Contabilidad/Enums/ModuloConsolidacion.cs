using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Dominio.Contabilidad.Enums
{
    public enum ModuloConsolidacion
    {
        Ventas = 1,
        Compras = 2,
        Cobranzas = 3,
        Pagos = 4,
        Fondos = 5
    }

    public static class ModuloConsolidacionExtensions
    {
        public static IEnumerable<int> ObtenerTiposComprobante(this ModuloConsolidacion modulo) =>
            modulo switch
            {
                ModuloConsolidacion.Ventas => new[]
                {
                    (int)TipoComprobante.VtsFactura,
                    (int)TipoComprobante.VtsNotaDebito,
                    (int)TipoComprobante.VtsNotaCredito
                },
                ModuloConsolidacion.Compras => new[]
                {
                    (int)TipoComprobante.CpsFactura,
                    (int)TipoComprobante.CpsNotaDebito,
                    (int)TipoComprobante.CpsNotaCredito
                },
                ModuloConsolidacion.Cobranzas => new[]
                {
                    (int)TipoComprobante.VtsRecibo,
                    (int)TipoComprobante.VtsAjuste
                },
                ModuloConsolidacion.Pagos => new[]
                {
                    (int)TipoComprobante.CpsPago,
                    (int)TipoComprobante.CpsAjuste
                },
                ModuloConsolidacion.Fondos => new[]
                {
                    (int)TipoComprobante.FdsIngreso,
                    (int)TipoComprobante.FdsEgreso,
                    (int)TipoComprobante.FdsDepositoChq,
                    (int)TipoComprobante.FdsDepositoEfvo,
                    (int)TipoComprobante.FdsCaucion,
                    (int)TipoComprobante.FdsVenta,
                    (int)TipoComprobante.FdsRechazo,
                    (int)TipoComprobante.FdsDebitoBancario,
                    (int)TipoComprobante.FdsTransferencia,
                    (int)TipoComprobante.FdsReingreso,
                    (int)TipoComprobante.FdsDevolucion,
                    (int)TipoComprobante.FdsAnulacion,
                    // TODO: FdsCompraVenta no existe en el enum legacy.
                    // Si se incorpora en el futuro, agregar aquí:
                    // (int)TipoComprobante.FdsCompraVenta,
                    (int)TipoComprobante.FdsTarjetas
                },
                _ => Array.Empty<int>()
            };

        public static string ObtenerNombre(this ModuloConsolidacion modulo) =>
            modulo switch
            {
                ModuloConsolidacion.Ventas => "Ventas",
                ModuloConsolidacion.Compras => "Compras",
                ModuloConsolidacion.Cobranzas => "Cobranzas",
                ModuloConsolidacion.Pagos => "Pagos",
                ModuloConsolidacion.Fondos => "Fondos",
                _ => modulo.ToString()
            };

        public static string ObtenerDetalleResumen(this ModuloConsolidacion modulo, int mes, int anio) =>
            modulo switch
            {
                ModuloConsolidacion.Ventas => $"Ventas del período {mes:D2}/{anio}",
                ModuloConsolidacion.Compras => $"Compras del período {mes:D2}/{anio}",
                ModuloConsolidacion.Cobranzas => $"Cobranzas del período {mes:D2}/{anio}",
                ModuloConsolidacion.Pagos => $"Pagos del período {mes:D2}/{anio}",
                ModuloConsolidacion.Fondos => $"Movimientos de fondos del período {mes:D2}/{anio}",
                _ => $"Período {mes:D2}/{anio}"
            };
    }
}
