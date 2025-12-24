namespace DemoWebAPI_2.DTO
{
    public class StudentQueryDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; }  
        public int? ClassId { get; set; }
    }
}
