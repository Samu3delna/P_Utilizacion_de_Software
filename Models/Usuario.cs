using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P_Utilizacion_de_Software.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string ContrasenaHash { get; set; } = string.Empty;

        [Required]
        public RolUsuario Rol { get; set; }

        public ICollection<Proyecto>? ProyectosCreados { get; set; }
        public ICollection<Tarea>? TareasAsignadas { get; set; }
        public ICollection<ProyectoParticipantes>? ProyectosParticipa { get; set; }
    }
}