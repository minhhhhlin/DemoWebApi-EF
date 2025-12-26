using DemoWebAPI_2.Entity;
using DemoWebAPI_2.DTO;
using System;

namespace DemoWebAPI_2.Service
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        String Login(string username, string password);
        void Register(string username, string password);
    }
}
