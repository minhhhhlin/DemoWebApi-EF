namespace DemoWebAPI_2.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
