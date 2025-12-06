using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using P_Utilizacion_de_Software.Data;
using P_Utilizacion_de_Software.Models;

namespace P_Utilizacion_de_Software.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Reporte para Profesor: Avance de proyectos
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> ReporteProfesor()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var proyectos = await _context.Proyectos
                .Where(p => p.ProfesorCreadorId == userId)
                .Include(p => p.Tareas)
                .ToListAsync();

            return View(proyectos);
        }

        // Reporte Personal para Estudiante
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> ReportePersonal()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var misTareas = await _context.Tareas
                .Where(t => t.EstudianteAsignadoId == userId)
                .Include(t => t.Proyecto)
                .ToListAsync();

            return View(misTareas);
        }
    }
}