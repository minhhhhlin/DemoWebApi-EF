using DemoWebAPI_2.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await  _classService.GetAllClasses();
            return Ok(classes);
        }
        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudentsByClassId(int id)
        {
            var students = await _classService.GetStudentsByClassId(id);
            return Ok(students);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var classEntity = await _classService.GetById(id);
            if (classEntity == null)
            {
                return NotFound();
            }
            return Ok(classEntity);
        }
    }
}
