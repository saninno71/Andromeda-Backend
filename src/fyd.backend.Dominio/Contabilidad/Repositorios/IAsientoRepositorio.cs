using fyd.backend.Dominio.Contabilidad.Entidades;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IAsientoRepositorio
    {
        // --- CRUD básico ---
        Task Agregar(Asiento asiento);
        void Eliminar(Asiento asiento);
        Task<Asiento?> ObtenerPorId(int id);

        // --- Validaciones de Cuenta Contable (B1, B2, B3) ---
        Task<bool> ExisteCuenta(int cuentaId);
        Task<int?> ObtenerEmpresaIdDeCuenta(int cuentaId);
        Task<bool> CuentaPermiteAsiento(int cuentaId);

        // --- Validaciones de Cliente (B4, B5) ---
        Task<bool> ExisteCliente(int clienteId);
        Task<int?> ObtenerEmpresaIdDeCliente(int clienteId);

        // --- Validaciones de Proveedor (B6, B7) ---
        Task<bool> ExisteProveedor(int proveedorId);
        Task<int?> ObtenerEmpresaIdDeProveedor(int proveedorId);

        // --- Validaciones de Caja (B8, B9) ---
        Task<bool> ExisteCaja(int cajaId);
        Task<int?> ObtenerEmpresaIdDeCaja(int cajaId);

        // --- Validaciones de Período y Comprobante (B11, B12, B13, B14) ---
        Task<bool> PeriodoEstaCerrado(DateTime fecha);
        Task<bool> TieneComprobantePadre(int asientoId);
        Task<bool> TieneVinculaciones(int asientoId);
        Task<bool> TieneAplicacionesEnCtaCte(int comprobanteId);
        Task<bool> CuentaTieneSubcuentaCaja(int cuentaId);
        Task<bool> CajaBancariaEstaConciliada(int cajaId);
        Task<bool> PeriodoEstaRenumerado(DateTime fecha);
        Task<bool> TieneNumeroAsientoAsignado(int comprobanteId);
    }
}
