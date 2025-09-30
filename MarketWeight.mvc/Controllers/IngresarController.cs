using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using System.Threading.Tasks;

namespace MarketWeight.mvc.Controllers
{
    [ServiceFilter(typeof(CargarSaldoFilter))]
    public class IngresarController : Controller
    {
        private readonly IRepoUsuario _repoUsuario;
        public IngresarController(IRepoUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Cantidad)
        {
            // Reemplazar coma por punto
            Cantidad = Cantidad.Replace(',', '.');
            var cantidadDecimal = decimal.Parse(Cantidad, System.Globalization.CultureInfo.InvariantCulture);
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
                return NotFound($"No se Inicio Sesion! ERROR!{idUsuario}");

            await _repoUsuario.IngresoAsync(Convert.ToUInt16(idUsuario), cantidadDecimal);

            return RedirectToAction("Index", "Ingresar");
        }

    }
}
