using AutoMapper.Configuration.Annotations;
using Microsoft.IdentityModel.Tokens;
using ScrumStandUpTracker_1.Models;
using ScrumStandUpTracker_1.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScrumStandUpTracker_1.Service
{
    public class DeveloperService
    {
        private readonly IDeveloperRepository _repository;
        private readonly IConfiguration _configuration;
        public DeveloperService(IDeveloperRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public async Task<bool> RegisterDeveloper(string username, string password)
        {
            var Existing = await _repository.GetDeveloper(username);
            if (Existing != null)
            {
                return false;
            }
            CreatePasswordHash(password, out byte[] PasswordHash, out byte[] PasswordSalt);
            var developer = new Developer
            {
                Username = username,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt
            };
            await _repository.AddDeveloper(developer);
            return true;
        }
        public async Task<Developer> LoginDeveloper(string username, string password)
        {
            var developer = await _repository.GetDeveloper(username);
            if (developer == null)
            {
                return null;
            }
            if(!VerifyPasswordHash(password,developer.PasswordHash, developer.PasswordSalt))
            {
                return null;
            }
            return developer;
        }
        public string GenerateJwtToken(Developer developer)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,developer.DeveloperId.ToString()),
                new Claim(ClaimTypes.Name, developer.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }   
        public void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password,byte[] StoredHash,byte[] StoredSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(StoredSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(StoredHash);
            }
        }
    }
}
