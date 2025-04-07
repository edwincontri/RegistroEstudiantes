namespace RegEstudiantes.Models
{
    public class Estudiante
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public DateTime fechaCreacion { get; set; } = DateTime.Now;
        public List<Materia> materias { get; set; } = new ();
    }
}
