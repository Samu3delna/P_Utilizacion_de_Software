using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace P_Utilizacion_de_Software.Models
{
    
    public class Usuario
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> 8aa98172eac0c1ab3fa9f62446475b4ba7fcb215
    }
}