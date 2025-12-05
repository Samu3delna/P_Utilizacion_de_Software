using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace P_Utilizacion_de_Software.Models
{
    public class Proyecto
    {
        public int ProyectoId { get; set; } // PK, IDENTITY

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinalizacion { get; set; }

        // Relación N:1 con Profesor (Usuario)
        public int ProfesorCreadorId { get; set; }
        public Usuario ProfesorCreador { get; set; }

        // Propiedades de Navegación
        public ICollection<Tarea> Tareas { get; set; }              // 1:N Proyecto -> Tareas
        public ICollection<ProyectoParticipantes> Participantes { get; set; } // N:M
    }
}