using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using System.Threading.Tasks;
using MarketWeight.mvc.ViewModels;


[ServiceFilter(typeof(CargarSaldoFilter))]
public class VenderMonedaController : Controller
{
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoMoneda _repoMoneda;

    public VenderMonedaController(IRepoUsuario repoUsuario, IRepoMoneda repoMoneda)
    {
        _repoUsuario = repoUsuario;
        _repoMoneda = repoMoneda;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
        if (idUsuario == null)
            return RedirectToAction("Index", "Login");

        // traigo monedas disponibles para el usuario
        var usuarioMonedas = await _repoUsuario.ObtenerUsuarioMonedaCondicionAsync((uint)idUsuario);

        // obtengo detalle de cada moneda
        var lista = new List<VMUsuarioMoneda>();
        foreach (var um in usuarioMonedas)
        {
            var moneda = await _repoMoneda.DetalleAsync(um.idMoneda);
            lista.Add(new VMUsuarioMoneda
            {
                Nombre = moneda.Nombre,
                Url = moneda.Url,
                Cantidad = um.Cantidad,
                IdMoneda = um.idMoneda
            });
        }

        return View(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Vender(uint idMoneda, string cantidad)
    {
        cantidad = cantidad.Replace(',', '.');
        var cantidadDecimal = decimal.Parse(cantidad, System.Globalization.CultureInfo.InvariantCulture);
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
        if (idUsuario == null)
            return RedirectToAction("Index", "Login");

        try
        {
            await _repoUsuario.VenderAsync((uint)idUsuario, cantidadDecimal, idMoneda);
            TempData["Success"] = "Venta realizada con éxito";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction("Index");
    }
}
