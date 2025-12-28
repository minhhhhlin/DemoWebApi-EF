using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public interface IFileService
    {
        Task<String> UploadAvatarAsync(IFormFile file);
    }
}
