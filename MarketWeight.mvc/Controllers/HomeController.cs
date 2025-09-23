using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.mvc.Models;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepoUsuario _repoUsuario;

    public HomeController(ILogger<HomeController> logger, IRepoUsuario repoUsuario)
    {
        _logger = logger;
        _repoUsuario = repoUsuario;
    }

    public async Task<IActionResult> Index()
    {
        int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
        decimal saldo = 0;

        if (idUsuario != null)
        {
            var usuario = await _repoUsuario.DetalleAsync((uint)idUsuario);
            saldo = usuario?.Saldo ?? 0;
        }

        ViewBag.Saldo = saldo;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(uint IdUsuario)
    {
        return RedirectToAction("ComprarMoneda", "Compra", new { IdUsuario });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
