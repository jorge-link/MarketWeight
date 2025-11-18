using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.mvc.ViewModels;
using MarketWeight.mvc.Helpers;
using MarketWeight.Core;
using System.Data;



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

            if (usuario.IdUsuario != 0)
            HttpContext.Session.SetInt32("IdUsuario", (int)usuario.IdUsuario);

            if (usuario != null)
            {
                HttpContext.Session.SetString("EmailUsuario", email);
            }
            else
                HttpContext.Session.Remove("EmailUsuario");

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IdUsuario");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registro(VMRegistroUsuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Crear usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Email = model.Email,
                Password = model.Password,
                Saldo = 0
            };

            try
            {
                await _repoUsuario.AltaAsync(nuevoUsuario);
            }
            catch (ConstraintException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario.");
                return View(model);
            }

            // Redirigir al login
            return RedirectToAction("Index");
        }
    
    }
}
