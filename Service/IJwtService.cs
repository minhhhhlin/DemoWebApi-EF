using DemoWebAPI_2.Entity;
using DemoWebAPI_2.DTO;
using System;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Service
{
    public interface IJwtService
    {
        Task<string> GenerateToken(User user);
        Task<String> Login(string username, string password);
        Task Register(string username, string password);
    }
}
