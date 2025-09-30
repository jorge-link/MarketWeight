using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using System.Threading.Tasks;

namespace MarketWeight.mvc.Controllers
{ 
    [ServiceFilter(typeof(CargarSaldoFilter))]
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
        public async Task<IActionResult> ComprarMonedaPost(uint IdMoneda, string Cantidad)
        {
            Cantidad = Cantidad.Replace(',', '.');
            var cantidadDecimal = decimal.Parse(Cantidad, System.Globalization.CultureInfo.InvariantCulture);
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
                return NotFound($"No se Inicio Sesion! ERROR!{idUsuario}");

            var usuario = await _repoUsuario.DetalleAsync(Convert.ToUInt16(idUsuario));
            var moneda = await _repoMoneda.DetalleAsync(IdMoneda);
            if (moneda == null)
                return NotFound($"No se encontró la moneda con ID {IdMoneda}");

            await _repoUsuario.CompraAsync(Convert.ToUInt16(idUsuario), cantidadDecimal, IdMoneda);

            var resumen = new
            {
                Usuario = usuario.Nombre,
                Moneda = moneda.Nombre,
                Cantidad = cantidadDecimal,
                PrecioUnitario = moneda.Precio,
                Total = cantidadDecimal * moneda.Precio,
                Img = moneda.Url
            };

            return View("ResumenCompra", resumen);
        }

    }
}
