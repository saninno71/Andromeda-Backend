using fyd.backend.Dominio.General.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.General
{
    public record Error(string Codigo, string Descripción, TipoDeError Tipo);

    public record Resultado
    {
        public bool Exitoso { get; init; }
        public Error? Error { get; init; }

        protected Resultado(bool exitoso, Error? error)
        {
            Exitoso = exitoso;
            Error = error;
        }

        public static Resultado Exito() => new(true, null);
        public static Resultado Falla(Error error) => new(false, error);
    }

    public record Resultado<T> : Resultado
    {
        public T? Valor { get; init; }

        private Resultado(T? valor, bool exitoso, Error? error) : base(exitoso, error)
        {
            Valor = valor;
        }

        public static Resultado<T> Exito(T valor) => new(valor, true, null);
        public static new Resultado<T> Falla(Error error) => new(default, false, error);
    };
}
