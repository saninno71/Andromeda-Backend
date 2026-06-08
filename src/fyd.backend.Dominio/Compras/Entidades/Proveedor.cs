using System.Collections.Generic;
using System.Linq;
using fyd.backend.Dominio.Compras.Enums;
using fyd.backend.Dominio.Compras.Errores;
using fyd.backend.Dominio.Compras.Parametros;
using fyd.backend.Dominio.Compras.Valores;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Valores;

namespace fyd.backend.Dominio.Compras.Entidades
{
    public class Proveedor
    {
        public int Id { get; set; }
        public int Codigo { get; private set; }
        public int AgendaId { get; private set; }
        public InfoAgenda? InfoAgenda { get; private set; }
        public int ProveedorCcId { get; private set; }
        public int IvaCategoriaId { get; private set; }
        public int CondicionId { get; private set; }
        public int CategoriaId { get; private set; }
        public int CalificacionId { get; private set; }
        public int? CuentaId { get; private set; }
        public int? MonedaId { get; private set; }
        public float PorcDescuento1 { get; private set; }
        public float PorcDescuento2 { get; private set; }
        public OrigenProveedor Origen { get; private set; }
        public DateTime? FechaAlta { get; private set; }
        public DateTime? FechaBaja { get; private set; }
        public SituacionProveedor Situacion { get; private set; }
        public decimal ImpCredito { get; private set; }
        public bool EventualOk { get; private set; }
        public bool SujetoVinculadoOk { get; private set; }

        private readonly List<int> _lineas = new();
        public IReadOnlyCollection<int> Lineas => _lineas.AsReadOnly();

        private readonly List<InfoRetencion> _retenciones = new();
        public IReadOnlyCollection<InfoRetencion> Retenciones => _retenciones.AsReadOnly();

        private Proveedor() { } // Constructor vacío para EF Core (Mapeo) o Reflexión

        public static Resultado<Proveedor> Crear(ProveedorParametros parametros)
        {
            if (parametros.AgendaId <= 0 && parametros.InfoAgenda == null)
                return Resultado<Proveedor>.Falla(ProveedorError.DatosInvalidos);

            var validacion = ValidarInvariantes(parametros);
            if (!validacion.Exitoso)
                return Resultado<Proveedor>.Falla(validacion.Error!);

            var entidad = new Proveedor
            {
                Codigo = parametros.Codigo,
                AgendaId = parametros.AgendaId,
                InfoAgenda = parametros.InfoAgenda,
                ProveedorCcId = parametros.ProveedorCcId,
                IvaCategoriaId = parametros.IvaCategoriaId,
                CondicionId = parametros.CondicionId,
                CategoriaId = parametros.CategoriaId,
                CalificacionId = parametros.CalificacionId,
                CuentaId = parametros.CuentaId,
                MonedaId = parametros.MonedaId,
                PorcDescuento1 = parametros.PorcDescuento1,
                PorcDescuento2 = parametros.PorcDescuento2,
                Origen = parametros.Origen,
                FechaAlta = parametros.FechaAlta ?? DateTime.UtcNow,
                FechaBaja = parametros.FechaBaja,
                Situacion = parametros.Situacion,
                ImpCredito = parametros.ImpCredito,
                EventualOk = parametros.EventualOk,
                SujetoVinculadoOk = parametros.SujetoVinculadoOk
            };

            if (parametros.Lineas != null)
                entidad._lineas.AddRange(parametros.Lineas.Distinct());

            if (parametros.Retenciones != null)
                entidad._retenciones.AddRange(parametros.Retenciones);

            return Resultado<Proveedor>.Exito(entidad);
        }

        public Resultado Actualizar(ProveedorParametros parametros)
        {
            if (parametros.AgendaId <= 0 && parametros.InfoAgenda == null)
                return Resultado.Falla(ProveedorError.DatosInvalidos);

            var validacion = ValidarInvariantes(parametros);
            if (!validacion.Exitoso)
                return Resultado.Falla(validacion.Error!);

            Codigo = parametros.Codigo;
            AgendaId = parametros.AgendaId;
            InfoAgenda = parametros.InfoAgenda;
            ProveedorCcId = parametros.ProveedorCcId;
            IvaCategoriaId = parametros.IvaCategoriaId;
            CondicionId = parametros.CondicionId;
            CategoriaId = parametros.CategoriaId;
            CalificacionId = parametros.CalificacionId;
            CuentaId = parametros.CuentaId;
            MonedaId = parametros.MonedaId;
            PorcDescuento1 = parametros.PorcDescuento1;
            PorcDescuento2 = parametros.PorcDescuento2;
            Origen = parametros.Origen;
            FechaBaja = parametros.FechaBaja;
            Situacion = parametros.Situacion;
            ImpCredito = parametros.ImpCredito;
            EventualOk = parametros.EventualOk;
            SujetoVinculadoOk = parametros.SujetoVinculadoOk;

            _lineas.Clear();
            if (parametros.Lineas != null)
                _lineas.AddRange(parametros.Lineas.Distinct());

            _retenciones.Clear();
            if (parametros.Retenciones != null)
                _retenciones.AddRange(parametros.Retenciones);

            return Resultado.Exito();
        }

        private static Resultado ValidarInvariantes(ProveedorParametros p)
        {
            if (p.FechaBaja.HasValue && p.FechaAlta.HasValue && p.FechaAlta.Value > p.FechaBaja.Value)
                return Resultado.Falla(ProveedorError.FechasInvalidas);

            if (p.PorcDescuento1 < 0 || p.PorcDescuento1 > 100 || p.PorcDescuento2 < 0 || p.PorcDescuento2 > 100)
                return Resultado.Falla(ProveedorError.PorcentajeDescuentoInvalido);

            if (p.Codigo < 0)
                return Resultado.Falla(ProveedorError.CodigoInvalido);

            return Resultado.Exito();
        }
    }
}
