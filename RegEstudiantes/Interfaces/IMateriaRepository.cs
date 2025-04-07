using RegEstudiantes.Models;

namespace RegEstudiantes.Interfaces
{
    public interface IMateriaRepository
    {
        Task<Materia> GetMateriaId(int id);
        Task<List<Materia>> GetMaterias();
        Task<List<Materia>> GetListIds(List<int> ids);
        Task<List<Materia>> GetProfesorId(int profesorId);
    }
}
