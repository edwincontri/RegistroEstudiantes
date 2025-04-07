using Microsoft.AspNetCore.Mvc;
using RegEstudiantes.Interfaces;

namespace RegEstudiantes.Controllers
{
    [Route("api/Materias")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _service;
        public MateriaController(IMateriaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _service.GetMaterias();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _service.GetMateriasId(id);
            return subject != null ? Ok(subject) : NotFound();
        }

        [HttpGet("profesor/{profesorId}")]
        public async Task<IActionResult> GetByProfessor(int professorId)
        {
            var subjects = await _service.GetMateriasProfesor(professorId);
            return Ok(subjects);
        }
    }
}
