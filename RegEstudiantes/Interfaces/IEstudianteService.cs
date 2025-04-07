using RegEstudiantes.Models;
using RegEstudiantes.Models.DTO;

namespace RegEstudiantes.Interfaces
{
    public interface IEstudianteService
    {
        public Task<Estudiante> CreaEstudiante(CreaEstudianteDto dto);
        public Task<List<Estudiante>> GetAllEstudiantes();
        public Task<Estudiante> GetEstudianteId(int id);
        public Task<Estudiante> ActualizaEstudiante(int id, CreaEstudianteDto dto);
        public Task<bool> EliminaEstudiante(int id);
        public Task<List<ClaseEstudiante>> GetClaseEstudiantes(int id);        
    }
}
