using RegEstudiantes.Interfaces;
using RegEstudiantes.Models;

namespace RegEstudiantes.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _profesorRepo;        

        public ProfesorService(IProfesorRepository profesorRepo)
        {
            _profesorRepo = profesorRepo;            
        }

        public async Task<List<Profesor>> GetProfesores()
        {
            var professors = await _profesorRepo.GetProfesor();
            return professors.ToList();
        }

        public async Task<Profesor> GetProfesorId(int id)
        {
            var professor = await _profesorRepo.GetProfesorID(id);
            return professor;
        }
    }
}
