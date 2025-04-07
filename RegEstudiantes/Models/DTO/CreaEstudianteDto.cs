namespace RegEstudiantes.Models.DTO
{
    public class CreaEstudianteDto
    {
        public string nombre { get; set; }
        public string email { get; set; }
        public List<int> materiaIds { get; set; }
    }
}
