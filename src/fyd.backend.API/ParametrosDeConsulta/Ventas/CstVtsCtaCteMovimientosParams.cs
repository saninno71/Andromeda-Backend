namespace fyd.backend.API.ParametrosDeConsulta.Ventas
{
    public record CstVtsCtaCteMovimientosParams(
        int Id,
        string EmpresaID,
        int ClienteCCID,
        int MonedaID,
        bool SoloPendienteOk,
        int FechaDesde,
        int FechaHasta,
        int FechaAplicacion,
        int FechaVtoDesde,
        int FechaVtoHasta,
        int FechaVtoAplicacion,
        int VendedorID,
        int CobradorID,
        string NumeraTipos,
        string Detalle
    );
}
