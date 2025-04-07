using Microsoft.AspNetCore.Mvc;
using RegEstudiantes.Interfaces;
using RegEstudiantes.Models.DTO;

namespace RegEstudiantes.Controllers
{
    [Route("api/Estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _service;
        public EstudiantesController(IEstudianteService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _service.GetAllEstudiantes();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _service.GetEstudianteId(id);
            return student != null ? Ok(student) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreaEstudianteDto dto)
        {
            try
            {
                var student = await _service.CreaEstudiante(dto);
                return CreatedAtAction(nameof(GetById), new { id = student.id }, student);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreaEstudianteDto dto)
        {
            try
            {
                var student = await _service.ActualizaEstudiante(id, dto);
                return student != null ? Ok(student) : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.EliminaEstudiante(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("ClaseEstudiante/{id}")]
        public async Task<IActionResult> ClaseEstudiante(int id)
        {
            try
            {
                var result = await _service.GetClaseEstudiantes(id);
                return Ok(result);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }           
        }
    }
}
