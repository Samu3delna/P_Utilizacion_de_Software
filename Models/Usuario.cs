namespace P_Utilizacion_de_Software.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // "Estudiante" o "Profesor"
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relaciones
        public ICollection<ProyectoParticipantes> ProyectosParticipantes { get; set; } = new List<ProyectoParticipantes>();
        public ICollection<Tarea> TareasAsignadas { get; set; } = new List<Tarea>();
    }
}