using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;

namespace MarketWeight.mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRepoUsuario _repoUsuario;

        public LoginController(IRepoUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        // Página de login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Procesar login
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Debe ingresar correo y contraseña";
                return View();
            }

            // Usamos ObtenerPorCondicionAsync para filtrar
            var usuarios = await _repoUsuario.ObtenerPorCondicionAsync($"email = '{email}' AND pass = '{password}'");
            var usuario = usuarios.FirstOrDefault();

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }

            // Guardamos IdUsuario en sesión
            HttpContext.Session.SetInt32("IdUsuario", (int)usuario.IdUsuario);

            // Redirigimos a Home/Index o a la página principal
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IdUsuario");
            return RedirectToAction("Index", "Home");
        }
    }
}
