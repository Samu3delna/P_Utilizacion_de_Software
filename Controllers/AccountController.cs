using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks; // Agregado para evitar errores con Task
using System.Collections.Generic; // Agregado para List<Claim>
using P_Utilizacion_de_Software.Models.ViewModels;
using P_Utilizacion_de_Software.Services;

namespace P_Utilizacion_de_Software.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AccountController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            // Si ya está logueado, lo mandamos al inicio
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool registroExitoso = await _usuarioService.RegistrarEstudianteAsync(model);
                if (registroExitoso)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError(string.Empty, "El correo ya está registrado.");
            }
            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioService.ValidarCredencialesAsync(model);
                if (usuario != null)
                {
                    // Crear la identidad del usuario (Claims)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim(ClaimTypes.Email, usuario.Correo),
                        new Claim(ClaimTypes.Role, usuario.Rol.ToString()) // IMPORTANTE: El Rol
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    // Crear la cookie de sesión
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
            }
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // GET: /Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}