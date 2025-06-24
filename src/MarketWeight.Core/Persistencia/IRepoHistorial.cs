namespace MarketWeight.Core.Persistencia;

public interface IRepoHistorial :
    IRepoAlta<Historial>,
    IRepoListado<Historial>,
    IRepoDetalle<Historial, uint>
{
    Task AltaAsync(Historial historial);
    Task<IEnumerable<Historial>> ObtenerAsync();
    Task<Historial?> DetalleAsync(uint id);
}