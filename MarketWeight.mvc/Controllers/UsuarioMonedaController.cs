using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using System.Linq;
using System.Threading.Tasks;

[ServiceFilter(typeof(CargarSaldoFilter))]
public class UsuarioMonedaController : Controller
{
    private readonly IRepoUsuario _repo;
    private readonly IRepoMoneda _repoM;

    public UsuarioMonedaController(IRepoUsuario repo, IRepoMoneda repoM)
    {
        _repo = repo;
        _repoM = repoM;
    }

    public async Task<IActionResult> Index()
    {
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
        if (idUsuario == null)
            return NotFound($"No se inició sesión. ERROR! {idUsuario}");

        var usuarioMonedas = await _repo.ObtenerUsuarioMonedaCondicionAsync((uint)idUsuario);

        var lista = usuarioMonedas.Select(um => {
            var moneda = _repoM.DetalleAsync(um.idMoneda).Result;
            return new VMUsuarioMoneda
            {
                IdMoneda = moneda.IdMoneda,
                Nombre = moneda.Nombre,
                Url = moneda.Url,
                Cantidad = um.Cantidad
            };
        }).ToList();

        return View(lista);
    }
}
