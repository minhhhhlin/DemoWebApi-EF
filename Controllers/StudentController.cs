using DemoWebAPI_2.DTO;
using DemoWebAPI_2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IFileService _fileService;
        public StudentController(IStudentService studentService, IFileService fileService)
        {
            _studentService = studentService;
            _fileService = fileService;
        }

        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);

        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllStudents()
        //{
        //    var students = await _studentService.GetAllStudents();
        //    return Ok(students);
        //}

        [Authorize]
        [HttpGet]
        public IActionResult GetStudents([FromQuery] StudentQueryDto q)
        {
            var students = _studentService.GetStudents(q);
            return Ok(students);
        }

        [Authorize]
        [HttpPost("{id}/avatar")]
        public async Task<IActionResult> UploadAvatar(
    int id,
    [FromForm] UploadAvatarDto dto)
        {
            await _studentService.UploadAvatarAsync(id, dto.File);
            return Ok("Upload avatar thành công");
        }

    }
}
