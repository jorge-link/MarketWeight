namespace MarketWeight.Core.Persistencia;

public interface IRepoUsuario :
    IRepoAlta<Usuario>,
    IRepoListado<Usuario>,
    IRepoDetalle<Usuario, uint>
{
    void Compra(uint idusuario, decimal cantidad, uint idmoneda);
    Task CompraAsync(uint idusuario, decimal cantidad, uint idmoneda);

    void Vender(uint idusuario, decimal cantidad, uint idmoneda);
    Task VenderAsync(uint idusuario, decimal cantidad, uint idmoneda);

    void Ingreso(uint idusuario, decimal saldo);
    Task IngresoAsync(uint idusuario, decimal saldo);

    void Transferencia(uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido);
    Task TransferenciaAsync(uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido);

    IEnumerable<Usuario> ObtenerPorCondicion(string condicion);
    Task<IEnumerable<Usuario>> ObtenerPorCondicionAsync(string condicion);

    IEnumerable<UsuarioMoneda> ObtenerUsuarioMoneda();
    Task<IEnumerable<UsuarioMoneda>> ObtenerUsuarioMonedaAsync();

    IEnumerable<UsuarioMoneda> ObtenerPorCondicionUsuarioMoneda(uint? userid, decimal? cantidad);
    Task<IEnumerable<UsuarioMoneda>> ObtenerPorCondicionUsuarioMonedaAsync(uint? userid, decimal? cantidad);

    Usuario? DetalleCompleto(uint idUsuario);
    Task<Usuario?> DetalleCompletoAsync(uint idUsuario);
}
