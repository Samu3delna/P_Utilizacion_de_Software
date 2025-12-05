namespace P_Utilizacion_de_Software.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        public required string Nombre { get; set; } 

        [Required, EmailAddress]
        public required string Correo { get; set; } 

        [Required]
        public required string ContrasenaHash { get; set; } 

        [Required]
        public RolUsuario Rol { get; set; } 

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // ... (El resto de las relaciones) ...
        public ICollection<ProyectoParticipantes> ProyectosParticipantes { get; set; } = new List<ProyectoParticipantes>();
        public ICollection<Tarea> TareasAsignadas { get; set; } = new List<Tarea>();
    }
}