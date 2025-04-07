using RegEstudiantes.Interfaces;
using RegEstudiantes.Models;
using RegEstudiantes.Models.DTO;

namespace RegEstudiantes.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepo;
        private readonly IMateriaRepository _materiaRepo;

        public EstudianteService(IEstudianteRepository estudianteRepo, IMateriaRepository materiaRepo)
        {
            _estudianteRepo = estudianteRepo;
            _materiaRepo = materiaRepo;
        }

        public async Task<Estudiante> CreaEstudiante(CreaEstudianteDto dto)
        {
            await ValidaMaterias(dto.materiaIds);
         
            var student = new Estudiante { nombre = dto.nombre, email = dto.email };
            var studentId = await _estudianteRepo.CreateEstudiante(student);
         
            await _estudianteRepo.AsignaMateriaEstudiante(studentId, dto.materiaIds);

            return await _estudianteRepo.GetEstudianteId(studentId);
        }

        public async Task<List<Estudiante>> GetAllEstudiantes() => await _estudianteRepo.GetAllEstudiantes();

        public async Task<Estudiante> GetEstudianteId(int id) => await _estudianteRepo.GetEstudianteId(id);

        public async Task<Estudiante> ActualizaEstudiante(int id, CreaEstudianteDto dto)
        {
            var student = await _estudianteRepo.GetEstudianteId(id);
            if (student == null) return null;

            student.nombre = dto.nombre ?? student.nombre;
            student.email = dto.email ?? student.email;

            if (dto.materiaIds != null && dto.materiaIds.Any())
            {
                await ValidaMaterias(dto.materiaIds);
                await _estudianteRepo.AsignaMateriaEstudiante(id, dto.materiaIds);
            }

            await _estudianteRepo.ActualizaEstudiante(student);
            return await _estudianteRepo.GetEstudianteId(id);
        }

        public async Task<bool> EliminaEstudiante(int id)
        {
            return await _estudianteRepo.EliminaEstudiante(id);
        }

        public async Task<List<ClaseEstudiante>> GetClaseEstudiantes(int id)
        {            
            var existeId = await _estudianteRepo.ExisteEstudiante(id);
            if (!existeId)
                throw new KeyNotFoundException("Estudiante no encontrado");

            var tieneMaterias = await _estudianteRepo.TieneMaterias(id);
            if (!tieneMaterias)
                throw new KeyNotFoundException("No tiene materias asignadas");

            return await _estudianteRepo.GetClaseEstudiante(id);
        }

        private async Task ValidaMaterias(List<int> materiaIds)
        {
            if (materiaIds.Count > 3)
                throw new InvalidOperationException("Solo puedes seleccionar 3 materias");
            
            var subjects = await _materiaRepo.GetListIds(materiaIds);
            if (subjects.GroupBy(s => s.profesorId).Any(g => g.Count() > 1))
                throw new InvalidOperationException("No puedes tener materias con el mismo profesor");
        }
    }
}
