using RegEstudiantes.Interfaces;
using RegEstudiantes.Models;

namespace RegEstudiantes.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepo;
        public MateriaService(IMateriaRepository materiaRepo) => _materiaRepo = materiaRepo;       
            


        public async Task<List<Materia>> GetMaterias()
        {
           return await _materiaRepo.GetMaterias();
        }

        public async Task<Materia> GetMateriasId(int id)
        {
           return await _materiaRepo.GetMateriaId(id);
        }

        public async Task<List<Materia>> GetMateriasProfesor(int professorId)
        {
            return await _materiaRepo.GetProfesorId(professorId);
        }
    }
}
