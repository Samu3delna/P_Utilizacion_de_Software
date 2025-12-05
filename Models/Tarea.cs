using System.ComponentModel.DataAnnotations;

namespace P_Utilizacion_de_Software.Models
{
    public class Tarea
    {
        public int TareaId { get; set; } // PK, IDENTITY

        [Required]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        [Required]
        public EstadoTarea Estado { get; set; }

        // Relación N:1 con Proyecto
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }

        // Relación N:1 con Estudiante (Usuario)
        public int EstudianteAsignadoId { get; set; }
        public Usuario EstudianteAsignado { get; set; }
    }
}