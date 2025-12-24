namespace DemoWebAPI_2.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ClassName { get; set; }
    }
    public class CreateStudentDto
    {
        public string FullName { get; set; }
        public int ClassId { get; set; }
    }
    public class UpdateStudentDto
    {
        public string FullName { get; set; }
        public int ClassId { get; set; }
    }
}
