using Microsoft.EntityFrameworkCore;
using P_Utilizacion_de_Software.Models; // Importante para reconocer tus clases

namespace P_Utilizacion_de_Software.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor necesario para recibir la configuración de Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =================================================================
        // DBSETS: Representan las tablas de tu Base de Datos
        // =================================================================

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<ProyectoParticipantes> ProyectoParticipantes { get; set; }

        // =================================================================
        // CONFIGURACIÓN ADICIONAL DEL MODELO
        // =================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la LLAVE COMPUESTA para la tabla intermedia
            // Esto le dice a EF Core que la PK está formada por ProyectoId + EstudianteId
            modelBuilder.Entity<ProyectoParticipantes>()
                .HasKey(pp => new { pp.ProyectoId, pp.EstudianteId });

            // Llamada a la clase base para no perder configuraciones por defecto
            base.OnModelCreating(modelBuilder);
        }
    }
}