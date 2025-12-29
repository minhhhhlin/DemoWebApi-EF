using DemoWebAPI_2.DTO;
using DemoWebAPI_2.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        public StudentService(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        // EF core LinQ to query data
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

        public async Task<object> GetStudents(StudentQueryDto q)
        {
            //Tạo query, chưa chạy DB
            IQueryable<Student> query = _context.Students;
            //Filter theo lớp
            if (q.ClassId.HasValue)
            {
                query = query.Where(s => s.ClassId == q.ClassId.Value);
            }
            //TÌm kiếm theo tên
            if(!string.IsNullOrEmpty(q.Search))
            {
                query = query.Where(s => s.FullName.Contains(q.Search));
            }
            //Sort
            if(!string.IsNullOrEmpty(q.SortBy))
            {
                if(q.SortBy.ToLower() == "fullname")
                {
                    query = q.Order?.ToLower() == "desc" ?
                        query.OrderByDescending(s => s.FullName) :
                        query.OrderBy(s => s.FullName);
                }
                else
                {
                    //Sort theo Id mặc định
                    query = q.Order?.ToLower() == "desc" ?
                        query.OrderByDescending(s => s.Id) :
                        query.OrderBy(s => s.Id);
                }
            }
            int totalRecords = await query.CountAsync();
            //Pagination
            var students = await query
                .Skip((q.Page - 1) * q.PageSize)
                .Take(q.PageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    ClassName = s.Class.Name 
                }).ToListAsync().ConfigureAwait(false); //Chạy query       
            return new
            {
                totalRecords = totalRecords,
                Page = q.Page,
                PageSize = q.PageSize,
                TotalPages = (int)System.Math.Ceiling((double)totalRecords / q.PageSize),
                Data = students
            };
        }
        //IFormFile
        public async Task UploadAvatarAsync(int studentId, IFormFile file)
        {
            //Kiểm tra student tồn tại
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
                throw new Exception("Student không tồn tại");

            //Upload file
            var avatarUrl = await _fileService.UploadAvatarAsync(file);

            //Update DB
            student.AvatarUrl = avatarUrl;
            await _context.SaveChangesAsync();
        }
        
        public async Task AddStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                ClassId = dto.ClassId
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                throw new Exception("Student không tồn tại");
            student.FullName = dto.FullName;
            student.ClassId = dto.ClassId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                throw new Exception("Student không tồn tại");
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}

