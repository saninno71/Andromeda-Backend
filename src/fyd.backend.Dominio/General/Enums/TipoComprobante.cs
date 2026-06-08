namespace fyd.backend.Dominio.General.Enums
{
    public enum TipoComprobante
    {
        // Contabilidad
        CtbAsiento = 1101,

        // Ventas
        VtsCotizacion = 2201,
        VtsPedido = 2202,
        VtsProforma = 2203,
        VtsRemito = 2204,
        VtsFactura = 2205,
        VtsNotaCredito = 2206,
        VtsNotaDebito = 2207,
        VtsRemitoFacturado = 2208,

        // Compras
        CpsRequerimiento = 3200,
        CpsCotizacion = 3201,
        CpsPedido = 3202,
        CpsProforma = 3203,
        CpsRemito = 3204,
        CpsFactura = 3205,
        CpsNotaCredito = 3206,
        CpsNotaDebito = 3207,

        // Cuenta corriente clientes
        VtsProvisorio = 2301,
        VtsRecibo = 2302,
        VtsAjuste = 2303,

        // Cuenta corriente proveedores
        CpsProvisorio = 3301,
        CpsPago = 3302,
        CpsAjuste = 3303,

        // Cuenta corriente de corredores
        CmsPago = 4302,
        CmsAjuste = 4303,

        // Caja y bancos
        FdsIngreso = 1401,
        FdsEgreso = 1402,
        FdsDepositoChq = 1403,
        FdsDepositoEfvo = 1404,
        FdsCaucion = 1405,
        FdsVenta = 1406,
        FdsRechazo = 1407,
        FdsDebitoBancario = 1408,
        FdsTransferencia = 1409,
        FdsReingreso = 1410,
        FdsDevolucion = 1411,
        FdsCajaDiaria = 1412,
        FdsAnulacion = 1413,
        FdsOtrasOperaciones = 1414,
        // TODO: FdsCompraVenta no existe en el enum legacy.
        // Si se incorpora en el futuro, agregarlo aquí con su valor correspondiente
        // y sumarlo a ModuloConsolidacion.ObtenerTiposComprobante(Fondos).
        FdsTarjetas = 1415,

        // Stock
        StkProduccion = 1501,
        StkAjuste = 1502,
        StkRemito = 1503,
        StkTransformacion = 1504,
        StkConsignacion = 1505,
        StkPlanProduccion = 1506,
        StkGuiaTraslado = 1507,
        StkOperacionTransporte = 1508,
        StkRemesa = 1521,
        StkControlExpedicion = 1531,
        StkPicking = 1541,
        StkReserva = 1542,

        // Producción
        PdcOrdenProduccion = 1601,
        PdcParteProduccion = 1602,

        // Certificados de retención
        RetencionIva = 3612,
        RetencionIIBB = 3622,
        RetencionGanancias = 3632,
        RetencionSuss = 3642,
    }
}
