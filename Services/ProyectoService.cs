using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Data;
using P_Utilizacion_de_Software.Models;

namespace P_Utilizacion_de_Software.Services
{
    public class ProyectoService
    {
        private readonly ApplicationDbContext _context;

        public ProyectoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Obtener proyectos creados por un Profesor
        public async Task<List<Proyecto>> GetProyectosByProfesorIdAsync(int profesorId)
        {
            return await _context.Proyectos
                .Include(p => p.Tareas) // Cargamos las tareas para ver el progreso
                .Include(p => p.Participantes) // Cargamos participantes
                .Where(p => p.ProfesorCreadorId == profesorId)
                .ToListAsync();
        }

        // 2. Obtener proyectos donde participa un Estudiante (AQUÍ ESTABA EL ERROR)
        public async Task<List<Proyecto>> GetProyectosByEstudianteIdAsync(int estudianteId)
        {
            // CORRECCIÓN: Consultamos la tabla Proyectos directamente y filtramos
            // a aquellos que tengan al estudiante en su lista de Participantes.
            return await _context.Proyectos
                .Include(p => p.ProfesorCreador) // Necesario para mostrar quién lo creó
                .Include(p => p.Tareas)          // Necesario para reportes o ver tareas
                .Where(p => p.Participantes!.Any(pp => pp.EstudianteId == estudianteId))
                .ToListAsync();
        }

        // 3. Obtener detalles de un proyecto específico
        public async Task<Proyecto?> GetProyectoByIdAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.ProfesorCreador)
                .Include(p => p.Participantes!)
                    .ThenInclude(pp => pp.Estudiante) // Cargar datos del estudiante
                .Include(p => p.Tareas!)
                    .ThenInclude(t => t.EstudianteAsignado) // Cargar datos del responsable de tarea
                .FirstOrDefaultAsync(m => m.ProyectoId == id);
        }

        // 4. Crear Proyecto
        public async Task CreateProyectoAsync(Proyecto proyecto)
        {
            _context.Add(proyecto);
            await _context.SaveChangesAsync();
        }

        // 5. Editar Proyecto
        public async Task UpdateProyectoAsync(Proyecto proyecto)
        {
            _context.Update(proyecto);
            await _context.SaveChangesAsync();
        }

        // 6. Eliminar Proyecto
        public async Task DeleteProyectoAsync(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
                await _context.SaveChangesAsync();
            }
        }

        // 7. Agregar Estudiante al Proyecto
        public async Task AddEstudianteAsync(int proyectoId, int estudianteId)
        {
            // Verificamos si ya existe para no duplicar
            bool existe = await _context.ProyectoParticipantes
                .AnyAsync(pp => pp.ProyectoId == proyectoId && pp.EstudianteId == estudianteId);

            if (!existe)
            {
                var participacion = new ProyectoParticipantes
                {
                    ProyectoId = proyectoId,
                    EstudianteId = estudianteId
                };
                _context.Add(participacion);
                await _context.SaveChangesAsync();
            }
        }

        // 8. Obtener lista de estudiantes que NO están en el proyecto (Para el dropdown de agregar)
        public async Task<List<Usuario>> GetEstudiantesNoAsignadosAsync(int proyectoId)
        {
            // Obtenemos los IDs de los que YA están
            var idsEnProyecto = await _context.ProyectoParticipantes
                .Where(pp => pp.ProyectoId == proyectoId)
                .Select(pp => pp.EstudianteId)
                .ToListAsync();

            // Buscamos usuarios que sean Estudiantes Y que NO estén en esa lista
            return await _context.Usuarios
                .Where(u => u.Rol == RolUsuario.Estudiante && !idsEnProyecto.Contains(u.UsuarioId))
                .ToListAsync();
        }
    }
}