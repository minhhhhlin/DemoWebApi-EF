using DemoWebAPI_2.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentById(int id);
        Task<List<StudentDto>> GetAllStudents();
    }
}
