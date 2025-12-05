using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Models;
// Usar el namespace donde definiste tus modelos

// Ejemplo: GestorProyectosAcademicos.Data
namespace P_Utilizacion_de_Software.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets que mapean a las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<ProyectoParticipantes> ProyectoParticipantes { get; set; }

        // Método para configurar la llave compuesta N:M
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProyectoParticipantes>()
                .HasKey(pp => new { pp.ProyectoId, pp.EstudianteId });

            base.OnModelCreating(modelBuilder);
        }
    }
}