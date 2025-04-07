using Microsoft.AspNetCore.Mvc;
using RegEstudiantes.Interfaces;

namespace RegEstudiantes.Controllers
{
    [Route("api/Profesor")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _service;

        public ProfesorController(IProfesorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professors = await _service.GetProfesores();
            return Ok(professors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _service.GetProfesorId(id);
            return professor != null ? Ok(professor) : NotFound();
        }
    }
}
