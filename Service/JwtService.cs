using DemoWebAPI_2.DTO;
using DemoWebAPI_2.Entity;
using DemoWebAPI_2.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DemoWebAPI_2.Service
{
    public class JwtService : IJwtService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public JwtService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {   
            //Thông tin user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            //key bảo mật
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //ký token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //tạo token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public String Login(string username, string password)
        {
            var hash = PasswordHasher.Hash(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);
            if(user == null)
            {
                throw new Exception("Invalid username or password");
            }
            var Token = GenerateToken(user);
            return Token;
        }

        public void Register(string username, string password)
        {
            if(_context.Users.Any(x => x.Username == username))
                throw new Exception("User exists");
            var user = new User
            {
                Username = username,
                PasswordHash = PasswordHasher.Hash(password)
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

