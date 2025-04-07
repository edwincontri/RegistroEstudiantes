using RegEstudiantes.Models;

namespace RegEstudiantes.Interfaces
{
    public interface IEstudianteRepository
    {
        public Task<List<Estudiante>> GetAllEstudiantes();        
        public Task<int> CreateEstudiante(Estudiante estudiante);
        public Task<bool> AsignaMateriaEstudiante(int estudianteId, List<int> materiaIds);
        public Task<Estudiante> GetEstudianteId(int id);
        public Task<bool> ActualizaEstudiante(Estudiante estudiante);
        public Task<bool> EliminaEstudiante(int id);
        public Task<List<ClaseEstudiante>> GetClaseEstudiante(int id);
        public Task<bool> ExisteEstudiante(int id);
        public Task<bool> TieneMaterias(int id);
    }
}
