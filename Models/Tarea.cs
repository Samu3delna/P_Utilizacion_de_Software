using System.ComponentModel.DataAnnotations;

namespace P_Utilizacion_de_Software.Models
{
    public class Tarea
    {
        public int TareaId { get; set; } // Llave Primaria

        [Required(ErrorMessage = "El título es obligatorio.")]
        public required string Titulo { get; set; }

        public string? Descripcion { get; set; } // Puede ser nulo

        [Required(ErrorMessage = "La fecha límite es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }

        [Required]
        public EstadoTarea Estado { get; set; } // Enum: Pendiente, EnProgreso, Completada

        // --- Relaciones ---

        // FK: Proyecto al que pertenece
        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }

        // FK: Estudiante responsable
        public int EstudianteAsignadoId { get; set; }
        public Usuario? EstudianteAsignado { get; set; }
    }
}