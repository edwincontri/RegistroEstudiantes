namespace RegEstudiantes.Models
{
    public class Materia
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int creditos { get; set; } = 3;
        public int profesorId { get; set; }
        public Profesor profesor { get; set; }
    }
}
