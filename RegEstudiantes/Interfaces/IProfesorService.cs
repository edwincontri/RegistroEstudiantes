using RegEstudiantes.Models;

namespace RegEstudiantes.Interfaces
{
    public interface IProfesorService
    {
        Task<List<Profesor>> GetProfesores();
        Task<Profesor> GetProfesorId(int id);
    }
}
