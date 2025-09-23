using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using MarketWeight.mvc.Helpers;                                                                                                                                                                                                                                      

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
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return View();

            string hashedPassword = PasswordHelper.HashPassword(password);

            var usuarios = await _repoUsuario.ObtenerPorCondicionAsync(
                $"email = '{email}' AND pass = '{hashedPassword}'"
            );

            var usuario = usuarios.FirstOrDefault();
            if (usuario == null)
            {
                ViewBag.Error = "Email o contraseña incorrecta";
                return View();
            }

            HttpContext.Session.SetInt32("IdUsuario", (int)usuario.IdUsuario);
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
