using RegEstudiantes.Models;

namespace RegEstudiantes.Interfaces
{
    public interface IProfesorRepository
    {
        Task<Profesor> GetProfesorID(int id);
        Task<List<Profesor>> GetProfesor();
    }
}
