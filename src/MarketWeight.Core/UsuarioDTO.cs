namespace MarketWeight.Core;

public class UsuarioDTO
{
    public uint IdUsuario { get; set; }
    public string Nombre { get; set; } = default!;
    public string Apellido { get; set; } = default!;
    public string Email { get; set; } = default!;
    public decimal Saldo { get; set; }
}
