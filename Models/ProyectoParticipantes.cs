namespace P_Utilizacion_de_Software.Models
{
    public class ProyectoParticipantes
    {
        // Llave compuesta (definida en el DbContext)
        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }

        public int EstudianteId { get; set; }
        public Usuario? Estudiante { get; set; }
    }
}