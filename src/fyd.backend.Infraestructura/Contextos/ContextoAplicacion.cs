using fyd.backend.Infraestructura.Compras.Entidades;
using fyd.backend.Infraestructura.Contabilidad.Entidades;
using fyd.backend.Infraestructura.Fondos.Entidades;
using fyd.backend.Infraestructura.General.Entidades;
using fyd.backend.Infraestructura.Parametros.Entidades;
using fyd.backend.Infraestructura.Ventas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.ORM
{
    //Contexto utilizado para las operaciones de escritura.
    public class ContextoAplicacion(DbContextOptions<ContextoAplicacion> options) : DbContext(options)
    {
        #region Contabilidad
        public DbSet<CuentaContable> CuentasContables { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Comprobante> ComprobantesContables { get; set; }
        public DbSet<IndiceContable> IndicesContables { get; set; }
        public DbSet<AsientoComprobante> AsientoComprobantes { get; set; }
        #endregion

        #region Compras
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorArticulo> ProveedoresArticulos { get; set; }
        public DbSet<ProveedorLinea> ProveedoresLineas { get; set; }
        public DbSet<ProveedorRetencion> ProveedoresRetenciones { get; set; }
        public DbSet<ProveedorMemo> ProveedoresMemos { get; set; }
        public DbSet<CompraComprobante> CompraComprobantes { get; set; }
        public DbSet<CompraCuentaCorriente> CompraCuentasCorrientes { get; set; }
        #endregion

        #region Ventas
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteDatoAdicional> ClientesDatosAdicionales { get; set; }
        public DbSet<ClienteLinea> ClientesLineas { get; set; }
        public DbSet<ClienteMemo> ClientesMemos { get; set; }
        public DbSet<ClientePercepcion> ClientesPercepciones { get; set; }
        public DbSet<VentaComprobante> VentaComprobantes { get; set; }
        public DbSet<VentaCuentaCorriente> VentaCuentasCorrientes { get; set; }
        #endregion

        #region Fondos
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<FondoComprobante> FondoComprobantes { get; set; }
        public DbSet<FondoMovimiento> FondoMovimientos { get; set; }
        #endregion

        #region General
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<Bloqueo> Bloqueos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Imputacion> Imputaciones { get; set; }
        public DbSet<Numeracion> Numeraciones { get; set; }
        public DbSet<NumeraTipo> NumeraTipos { get; set; }
        #endregion

        #region Parametros
        public DbSet<VarioEmpresa> VariosEmpresas { get; set; }
        public DbSet<Vario> Varios { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- APLICAR LAS CONFIGURACIONES ---
            // Esto busca todas las clases que implementen IEntityTypeConfiguration 
            // en el assembly actual y las aplica. Es más limpio que llamar una por una.
            builder.ApplyConfigurationsFromAssembly(typeof(ContextoAplicacion).Assembly);

        }
    }
}
