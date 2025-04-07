using RegEstudiantes.Models;

namespace RegEstudiantes.Interfaces
{
    public interface IMateriaService
    {
        Task<List<Materia>> GetMaterias();
        Task<Materia> GetMateriasId(int id);
        Task<List<Materia>> GetMateriasProfesor(int professorId);
    }
}
