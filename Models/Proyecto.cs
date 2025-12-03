namespace P_Utilizacion_de_Software.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = "Activo"; // "Activo", "Completado", "Cancelado"
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relaciones
        public ICollection<ProyectoParticipantes> Participantes { get; set; } = new List<ProyectoParticipantes>();
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}