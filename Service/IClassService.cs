using DemoWebAPI_2.DTO;
using DemoWebAPI_2.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public interface IClassService
    {
        // Async/await
        Task<List<ClassDto>> GetAllClasses();
        Task<ClassDto> GetById(int id);
        Task<List<StudentDto>> GetStudentsByClassId(int classId);
    }
}
