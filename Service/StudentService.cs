using DemoWebAPI_2.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<StudentDto>> GetAllStudents()
        {
            var students = await _context.Students
                .Include(s => s.Class)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    ClassName = s.Class.Name
                }).ToListAsync();
            return students;    
        }

        public Task<StudentDto> GetStudentById(int id)
        {
            var student = _context.Students
                .Include(s => s.Class)
                .Where(s => s.Id == id)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    ClassName = s.Class.Name
                }).FirstOrDefaultAsync();
            return student;
        }
    }
}
