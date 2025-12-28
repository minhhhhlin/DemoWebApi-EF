using Microsoft.AspNetCore.Http;

namespace DemoWebAPI_2.DTO
{
    public class UploadAvatarDto
    {
        public IFormFile File { get; set; }
    }
}
