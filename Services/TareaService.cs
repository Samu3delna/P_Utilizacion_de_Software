using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Controllers;
using P_Utilizacion_de_Software.Data;
using P_Utilizacion_de_Software.Models;

namespace P_Utilizacion_de_Software.Services
{
    public class TareaService
    {
        private readonly ApplicationDbContext _context;

        public TareaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener una tarea por ID (incluyendo proyecto y estudiante)
        public async Task<Tarea?> GetTareaByIdAsync(int id)
        {
            return await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.EstudianteAsignado)
                .FirstOrDefaultAsync(m => m.TareaId == id);
        }

        // Crear una nueva tarea
        public async Task CreateTareaAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
        }

        // Actualizar una tarea existente
        public async Task UpdateTareaAsync(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
        }

        // Eliminar una tarea
        public async Task DeleteTareaAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
            }
        }
    }
}