namespace P_Utilizacion_de_Software.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; } = "Pendiente"; // "Pendiente", "En Progreso", "Completada"
        public string Prioridad { get; set; } = "Media"; // "Baja", "Media", "Alta"

        // Claves foráneas
        public int ProyectoId { get; set; }
        public int? UsuarioAsignadoId { get; set; }

        // Relaciones
        public Proyecto? Proyecto { get; set; }
        public Usuario? UsuarioAsignado { get; set; }
    }
}