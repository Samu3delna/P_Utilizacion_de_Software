using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P_Utilizacion_de_Software.Models; // Asegúrate de que este using esté para acceder a Proyecto y RolUsuario
using P_Utilizacion_de_Software.Services;
using System;
using System.Security.Claims;

namespace P_Utilizacion_de_Software.Controllers
{
    // [Authorize] obliga al usuario a iniciar sesión para acceder a CUALQUIER acción
    [Authorize]
    public class ProyectosController : Controller
    {
        private readonly ProyectoService _proyectoService;

        // Constructor para Inyección de Dependencias
        public ProyectosController(ProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        // =========================================================
        // ACCIÓN INDEX (Listado de Proyectos según Rol)
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1. Obtener el ID del usuario logueado
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                // Debería ser atrapado por [Authorize], pero es una buena práctica de seguridad.
                return RedirectToAction("Login", "Account");
            }

            List<Proyecto> proyectos = new List<Proyecto>();
            string rol = User.FindFirst(ClaimTypes.Role)?.Value ?? "Invitado";

            // 2. Cargar datos según el Rol
            if (User.IsInRole(RolUsuario.Profesor.ToString()))
            {
                // Si es Profesor, solo ve los proyectos que él ha creado.
                proyectos = await _proyectoService.GetProyectosByProfesorIdAsync(userId);
            }
            else if (User.IsInRole(RolUsuario.Estudiante.ToString()))
            {
                // Si es Estudiante, solo ve los proyectos en los que participa.
                proyectos = await _proyectoService.GetProyectosByEstudianteIdAsync(userId);
            }

            ViewData["Rol"] = rol; // Pasamos el rol a la vista para decidir qué botones mostrar
            return View(proyectos);
        }

        // =========================================================
        // ACCIÓN CREATE (Solo para Profesor)
        // =========================================================

        // [Authorize(Roles = "Profesor")] es la restricción CRÍTICA.
        // Si un estudiante intenta acceder, será redirigido a AccessDenied.
        [Authorize(Roles = "Profesor")]
        [HttpGet]
        public IActionResult Create()
        {
            // Muestra el formulario vacío para crear un nuevo proyecto
            return View(new Proyecto());
        }

        [Authorize(Roles = "Profesor")]
        [HttpPost]
        [ValidateAntiForgeryToken] // Protección contra ataques CSRF
        public async Task<IActionResult> Create(Proyecto proyecto)
        {
            // Validamos los campos básicos (nombre, fechas)
            if (ModelState.IsValid)
            {
                // Obtener ID del profesor logueado y asignarlo como creador
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                proyecto.ProfesorCreadorId = userId;

                // Llama al servicio para guardar el proyecto en la DB
                await _proyectoService.CreateProyectoAsync(proyecto);

                // Redirige al listado de proyectos
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores de validación, regresa al formulario con los datos ingresados
            return View(proyecto);
        }

        // ... Aquí se agregarían las acciones para Details, Edit y Delete ...
        // Estas también deben tener el atributo [Authorize(Roles = "Profesor")]
    }
}