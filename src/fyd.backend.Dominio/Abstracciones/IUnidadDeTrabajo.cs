namespace fyd.backend.Dominio.Abstracciones
{
    public interface IUnidadDeTrabajo
    {
        // Devuelve el número de filas afectadas en la DB
        Task<int> GuardarCambios();
    }
}
