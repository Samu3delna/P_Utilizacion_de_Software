using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Data;
using P_Utilizacion_de_Software.Models;
using P_Utilizacion_de_Software.Services;
using System.Security.Claims;

namespace P_Utilizacion_de_Software.Controllers
{
    [Authorize]
    public class ProyectosController : Controller
    {
        private readonly ProyectoService _proyectoService;
        private readonly UsuarioService _usuarioService;

        public ProyectosController(ProyectoService proyectoService, UsuarioService usuarioService)
        {
            _proyectoService = proyectoService;
            _usuarioService = usuarioService;
        }

        // GET: Proyectos (Lista)
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            List<Proyecto> proyectos;

            if (User.IsInRole("Profesor"))
            {
                proyectos = await _proyectoService.GetProyectosByProfesorIdAsync(userId);
            }
            else
            {
                proyectos = await _proyectoService.GetProyectosByEstudianteIdAsync(userId);
            }
            return View(proyectos);
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var proyecto = await _proyectoService.GetProyectoByIdAsync(id);
            if (proyecto == null) return NotFound();
            return View(proyecto);
        }

        // GET: Proyectos/Create
        [Authorize(Roles = "Profesor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyectos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Create(Proyecto proyecto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            proyecto.ProfesorCreadorId = userId;

            // Ignoramos validaciones de relaciones que aún no existen
            ModelState.Remove("ProfesorCreador");
            ModelState.Remove("Tareas");
            ModelState.Remove("Participantes");

            if (ModelState.IsValid)
            {
                await _proyectoService.CreateProyectoAsync(proyecto);
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyectos/Edit/5 (ESTE ES EL QUE TE FALTA PARA QUE NO DE 404)
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Edit(int id)
        {
            var proyecto = await _proyectoService.GetProyectoByIdAsync(id);
            if (proyecto == null) return NotFound();

            // Solo el creador puede editar
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (proyecto.ProfesorCreadorId != userId) return Forbid();

            return View(proyecto);
        }

        // POST: Proyectos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Edit(int id, Proyecto proyecto)
        {
            if (id != proyecto.ProyectoId) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            proyecto.ProfesorCreadorId = userId; // Mantener el creador original

            ModelState.Remove("ProfesorCreador");
            ModelState.Remove("Tareas");
            ModelState.Remove("Participantes");

            if (ModelState.IsValid)
            {
                await _proyectoService.UpdateProyectoAsync(proyecto);
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyectos/Delete/5 (ESTE TAMBIÉN FALTABA)
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Delete(int id)
        {
            var proyecto = await _proyectoService.GetProyectoByIdAsync(id);
            if (proyecto == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (proyecto.ProfesorCreadorId != userId) return Forbid();

            return View(proyecto);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _proyectoService.DeleteProyectoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Métodos extra para agregar estudiantes
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> AddStudent(int id)
        {
            var estudiantes = await _proyectoService.GetEstudiantesNoAsignadosAsync(id);
            ViewData["ProyectoId"] = id;
            ViewBag.EstudianteId = new SelectList(estudiantes, "UsuarioId", "Nombre");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> AddStudent(int ProyectoId, int EstudianteId)
        {
            await _proyectoService.AddEstudianteAsync(ProyectoId, EstudianteId);
            return RedirectToAction("Details", new { id = ProyectoId });
        }
    }
}