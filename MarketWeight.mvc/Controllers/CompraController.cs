using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using System.Threading.Tasks;

namespace MarketWeight.mvc.Controllers
{
    public class CompraController : Controller
    {
        private readonly IRepoUsuario _repoUsuario;
        private readonly IRepoMoneda _repoMoneda;

        public CompraController(IRepoUsuario repoUsuario, IRepoMoneda repoMoneda)
        {
            _repoUsuario = repoUsuario;
            _repoMoneda = repoMoneda;
        }

        // Mostrar formulario
        [HttpGet]
        public async Task<IActionResult> ComprarMoneda()
        {
            var monedas = await _repoMoneda.ObtenerAsync();
            var vm = new VMComprarMoneda(monedas);
            return View(vm);
        }

        // Procesar compra
        [HttpPost]
        public async Task<IActionResult> ComprarMoneda(uint IdUsuario, uint IdMoneda, string Cantidad)
        {
            // Reemplazar coma por punto
            Cantidad = Cantidad.Replace(',', '.');
            var cantidadDecimal = decimal.Parse(Cantidad, System.Globalization.CultureInfo.InvariantCulture);

            var usuario = await _repoUsuario.DetalleAsync(IdUsuario);
            if (usuario == null)
                return NotFound($"No se encontró el usuario con ID {IdUsuario}");

            var moneda = await _repoMoneda.DetalleAsync(IdMoneda);
            if (moneda == null)
                return NotFound($"No se encontró la moneda con ID {IdMoneda}");

            await _repoUsuario.CompraAsync(IdUsuario, cantidadDecimal, IdMoneda);

            var resumen = new
            {
                Usuario = usuario.Nombre,
                Moneda = moneda.Nombre,
                Cantidad = cantidadDecimal,
                PrecioUnitario = moneda.Precio,
                Total = cantidadDecimal * moneda.Precio
            };

            return View("ResumenCompra", resumen);
        }
    }
}
