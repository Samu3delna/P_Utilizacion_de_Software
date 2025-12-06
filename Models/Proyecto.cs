using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P_Utilizacion_de_Software.Models
{
    public class Proyecto
    {
        public int ProyectoId { get; set; } // Llave Primaria

        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        public required string Nombre { get; set; }

        public string? Descripcion { get; set; } // Puede ser nulo

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaFinalizacion { get; set; }

        // --- Relaciones ---

        // FK: Profesor que creó el proyecto
        public int ProfesorCreadorId { get; set; }
        public Usuario? ProfesorCreador { get; set; }

        // Colección de tareas del proyecto
        public ICollection<Tarea>? Tareas { get; set; }

        // Colección de participantes (Estudiantes)
        public ICollection<ProyectoParticipantes>? Participantes { get; set; }
    }
}