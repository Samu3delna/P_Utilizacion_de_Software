using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using P_Utilizacion_de_Software.Models;
using P_Utilizacion_de_Software.Services;
using P_Utilizacion_de_Software.Data;

namespace P_Utilizacion_de_Software.Controllers
{
    [Authorize]
    public class TareasController : Controller
    {
        private readonly TareaService _tareaService;
        private readonly ApplicationDbContext _context;

        public TareasController(TareaService tareaService, ApplicationDbContext context)
        {
            _tareaService = tareaService;
            _context = context;
        }

        // GET: /Tareas/Create?proyectoId=5
        public IActionResult Create(int proyectoId)
        {
            ViewData["ProyectoId"] = proyectoId;

            // Si es Profesor, puede ver lista de estudiantes para asignar
            if (User.IsInRole("Profesor"))
            {
                // Idealmente filtrar solo estudiantes de este proyecto
                var estudiantesDelProyecto = _context.ProyectoParticipantes
                    .Where(pp => pp.ProyectoId == proyectoId)
                    .Select(pp => pp.Estudiante)
                    .ToList();

                ViewData["EstudianteAsignadoId"] = new SelectList(estudiantesDelProyecto, "UsuarioId", "Nombre");
            }

            return View();
        }

        // POST: /Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarea tarea)
        {
            // Lógica: Si es estudiante, se asigna a sí mismo obligatoriamente
            if (User.IsInRole("Estudiante"))
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                tarea.EstudianteAsignadoId = userId;

                // Limpiar validación del modelo para este campo ya que lo asignamos manual
                ModelState.Remove("EstudianteAsignadoId");
            }

            if (ModelState.IsValid)
            {
                await _tareaService.CreateTareaAsync(tarea);
                TempData["Mensaje"] = "Tarea creada exitosamente.";
                return RedirectToAction("Details", "Proyectos", new { id = tarea.ProyectoId });
            }

            // Recargar datos si falla
            ViewData["ProyectoId"] = tarea.ProyectoId;
            if (User.IsInRole("Profesor"))
            {
                var estudiantesDelProyecto = _context.ProyectoParticipantes
                   .Where(pp => pp.ProyectoId == tarea.ProyectoId)
                   .Select(pp => pp.Estudiante)
                   .ToList();
                ViewData["EstudianteAsignadoId"] = new SelectList(estudiantesDelProyecto, "UsuarioId", "Nombre", tarea.EstudianteAsignadoId);
            }

            return View(tarea);
        }

        // GET: /Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _tareaService.GetTareaByIdAsync(id.Value);
            if (tarea == null) return NotFound();

            // Seguridad: Estudiante solo edita SUS tareas
            if (User.IsInRole("Estudiante"))
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                if (tarea.EstudianteAsignadoId != userId) return Forbid();
            }

            // Cargar lista para el select (si es profesor)
            if (User.IsInRole("Profesor"))
            {
                var estudiantes = _context.ProyectoParticipantes
                   .Where(pp => pp.ProyectoId == tarea.ProyectoId)
                   .Select(pp => pp.Estudiante)
                   .ToList();
                ViewData["EstudianteAsignadoId"] = new SelectList(estudiantes, "UsuarioId", "Nombre", tarea.EstudianteAsignadoId);
            }

            return View(tarea);
        }

        // POST: /Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarea tarea)
        {
            if (id != tarea.TareaId) return NotFound();

            if (ModelState.IsValid)
            {
                // Si es estudiante, recuperar la tarea original para no permitir cambios prohibidos
                // (como cambiar fecha límite o título), solo el estado.
                if (User.IsInRole("Estudiante"))
                {
                    var tareaOriginal = await _context.Tareas.AsNoTracking().FirstOrDefaultAsync(t => t.TareaId == id);
                    if (tareaOriginal != null)
                    {
                        tarea.Titulo = tareaOriginal.Titulo;
                        tarea.Descripcion = tareaOriginal.Descripcion;
                        tarea.FechaLimite = tareaOriginal.FechaLimite;
                        tarea.ProyectoId = tareaOriginal.ProyectoId;
                        tarea.EstudianteAsignadoId = tareaOriginal.EstudianteAsignadoId;
                        // Solo permitimos que cambie 'Estado'
                    }
                }

                await _tareaService.UpdateTareaAsync(tarea);
                return RedirectToAction("Details", "Proyectos", new { id = tarea.ProyectoId });
            }

            return View(tarea);
        }

        // GET: /Tareas/Delete/5 (Solo Profesor)
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await _tareaService.GetTareaByIdAsync(id);
            if (tarea != null)
            {
                await _tareaService.DeleteTareaAsync(id);
            }
            return RedirectToAction("Details", "Proyectos", new { id = tarea?.ProyectoId });
        }
    }
}