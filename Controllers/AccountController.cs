<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization; // Necesario para [Authorize]
using Microsoft.AspNetCore.Mvc;
using P_Utilizacion_de_Software.Models;
using P_Utilizacion_de_Software.Models.ViewModels;
using P_Utilizacion_de_Software.Services;
using System;
using System.Security.Claims;
>>>>>>> 8aa98172eac0c1ab3fa9f62446475b4ba7fcb215

namespace P_Utilizacion_de_Software.Controllers
{
    public class AccountController : Controller
    {
<<<<<<< HEAD
        public IActionResult Index()
        {
            return View();
        }
    }
}
=======
        private readonly UsuarioService _usuarioService;

        // Inyección de Dependencias
        public AccountController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // =========================================================
        // 1. REGISTRO (GET y POST)
        // =========================================================

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Muestra el formulario de registro (Estudiante por defecto)
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                // El servicio maneja el hasheo y guardado en la DB
                bool registroExitoso = await _usuarioService.RegistrarEstudianteAsync(model);

                if (registroExitoso)
                {
                    // Redirigir a Login después de un registro exitoso
                    return RedirectToAction("Login");
                }

                // Error: Correo ya existe
                ModelState.AddModelError(string.Empty, "El correo electrónico ya está registrado.");
            }
            return View(model);
        }

        // =========================================================
        // 2. LOGIN (GET y POST)
        // =========================================================

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioService.ValidarCredencialesAsync(model);

                if (usuario != null)
                {
                    // Crear la identidad del usuario para la sesión (Claim principal)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim(ClaimTypes.Email, usuario.Correo),
                        new Claim(ClaimTypes.Role, usuario.Rol.ToString()) // Clave para la autorización
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    // Inicia la sesión y establece la cookie
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credenciales inválidas. Revise su correo y contraseña.");
            }
            return View(model);
        }

        // =========================================================
        // 3. LOGOUT
        // =========================================================

        // El usuario debe estar autenticado para hacer Logout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
>>>>>>> 8aa98172eac0c1ab3fa9f62446475b4ba7fcb215
