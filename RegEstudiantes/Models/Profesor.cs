namespace RegEstudiantes.Models
{
    public class Profesor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Materia> materias { get; set; } = new();
    }
}
