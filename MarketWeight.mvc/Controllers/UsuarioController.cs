using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;    
using System.Linq;                         
using System.Threading.Tasks;                

public class UsuarioController : Controller
{
    private readonly IRepoUsuario _repo;

    public UsuarioController(IRepoUsuario repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = await _repo.ObtenerAsync();
        var usuariosDTO = usuarios.Select(u => new UsuarioDTO
        {
            IdUsuario = u.IdUsuario,
            Nombre = u.Nombre,
            Apellido = u.Apellido,
            Email = u.Email,
            Saldo = u.Saldo
        });

        return View(usuariosDTO);
    }
}
