using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Models;

namespace P_Utilizacion_de_Software.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor obligatorio para Inyección de Dependencias
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets: El acceso a tus tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<ProyectoParticipantes> ProyectoParticipantes { get; set; }

        // Configuración para la clave compuesta de la tabla ProyectoParticipantes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProyectoParticipantes>()
                .HasKey(pp => new { pp.ProyectoId, pp.EstudianteId });

            // Relación: Proyecto -> ProyectoParticipantes
            modelBuilder.Entity<ProyectoParticipantes>()
                .HasOne(pp => pp.Proyecto)
                .WithMany(p => p.Participantes)
                .HasForeignKey(pp => pp.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: Usuario -> ProyectoParticipantes
            modelBuilder.Entity<ProyectoParticipantes>()
                .HasOne(pp => pp.Estudiante)
                .WithMany(u => u.ProyectosParticipantes)
                .HasForeignKey(pp => pp.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: Proyecto -> Tarea
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Proyecto)
                .WithMany(p => p.Tareas)
                .HasForeignKey(t => t.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: Usuario -> Tarea (asignadas)
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioAsignado)
                .WithMany(u => u.TareasAsignadas)
                .HasForeignKey(t => t.UsuarioAsignadoId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}