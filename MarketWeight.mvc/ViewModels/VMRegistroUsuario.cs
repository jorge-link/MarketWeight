using System.ComponentModel.DataAnnotations;

public class VMRegistroUsuario
{
    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Apellido { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmarPassword { get; set; }
}
