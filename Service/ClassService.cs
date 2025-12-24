using DemoWebAPI_2.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _context;
        public ClassService(AppDbContext context)
        {
            _context = context;
        }
        // EF core LinQ to query data
        public async Task<List<ClassDto>> GetAllClasses()
        {
            var classes = await _context.Classes
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
            return classes;
        }

        public async Task<ClassDto> GetById(int id)
        {
            var classEntity = await _context.Classes
                .Where(c => c.Id == id)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();
            return classEntity;
        }

        public async Task<List<StudentDto>> GetStudentsByClassId(int classId)
        {
            var students = await _context.Students
                .Where(s => s.ClassId == classId)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    ClassName = s.Class.Name
                })
                .ToListAsync();
            return students;
        }
    }
}
