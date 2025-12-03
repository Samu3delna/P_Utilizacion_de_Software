namespace P_Utilizacion_de_Software.Models
{
    public class ProyectoParticipantes
    {
        public int ProyectoId { get; set; }
        public int EstudianteId { get; set; }
        public DateTime FechaUnion { get; set; } = DateTime.Now;
        public string Rol { get; set; } = "Participante"; // "Coordinador", "Participante"

        // Relaciones
        public Proyecto? Proyecto { get; set; }
        public Usuario? Estudiante { get; set; }
    }
}