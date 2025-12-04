namespace P_Utilizacion_de_Software.Models
{
    public class ProyectoParticipantes
    {
        // Llave compuesta
        public int ProyectoId { get; set; }
        public int EstudianteId { get; set; }

        // Propiedades de Navegación
        public Proyecto Proyecto { get; set; }
        public Usuario Estudiante { get; set; }
    }
}