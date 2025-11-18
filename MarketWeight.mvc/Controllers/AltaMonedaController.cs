using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using MarketWeight.mvc.Helpers;
using MarketWeight.Core;
using System.Data;

namespace MarketWeight.mvc.Controllers
{ 
    [ServiceFilter(typeof(CargarSaldoFilter))]
    public class AltaMonedaController : Controller
    {
        private readonly IRepoMoneda _repoMoneda;

        public AltaMonedaController(IRepoMoneda repoMoneda)
        {
            _repoMoneda = repoMoneda;
        }

        // GET: /AltaMoneda/Index
        [HttpGet]
        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("EmailUsuario");
            if (string.IsNullOrEmpty(email) || email.Trim().ToLower() != "admin@admin.com")
            {
                ViewBag.AccesoDenegado = true;
            }

            return View();
        }

       
       [HttpPost]
        public async Task<IActionResult> Index(VMAltaMoneda model)
        {
            string? email = HttpContext.Session.GetString("EmailUsuario");
            if (string.IsNullOrEmpty(email) || email.Trim().ToLower() != "admin@admin.com")
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
                return View(model);

            var nuevaMoneda = new Moneda
            {
                Nombre = model.Nombre,
                Precio = model.Precio,
                Cantidad = model.Cantidad,
                Url = model.Url
            };

            try
            {
                await _repoMoneda.AltaAsync(nuevaMoneda);
                ViewBag.MensajeExito = "La moneda se creó con éxito"; 
                ModelState.Clear();
                return View();
            }
            catch (ConstraintException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar la moneda");
                return View(model);
            }
        }

    }
}
