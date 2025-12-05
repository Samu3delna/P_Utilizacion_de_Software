using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace P_Utilizacion_de_Software.Models
{
    
    public class Usuario
    {
        public int UsuarioId { get; set; } // PK, IDENTITY

        [Required]
        public string Nombre { get; set; }

        [Required, EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string ContrasenaHash { get; set; } // Almacena el hash

        [Required]
        public RolUsuario Rol { get; set; }

        // Propiedades de Navegación (Relaciones)
        public ICollection<Proyecto> ProyectosCreados { get; set; } // 1:N Profesor -> Proyectos
        public ICollection<Tarea> TareasAsignadas { get; set; }     // 1:N Usuario -> Tareas
        public ICollection<ProyectoParticipantes> ProyectosParticipa { get; set; } // N:M
    }
}