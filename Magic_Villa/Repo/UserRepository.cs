using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_Villa.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUser.FirstOrDefault(c => c.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.LocalUser.FirstOrDefault(c=>c.UserName.ToLower() == loginRequestDto.UserName.ToLower()
            && c.Password == loginRequestDto.Password);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    Token = "",
                    User = null
                };
            }
            //
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponse = new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponse;
        }

        public async Task<LocalUser> Register(RegistrationRequestionDto registrationRequestionDto)
        {
            LocalUser user = new()
            {
                UserName = registrationRequestionDto.UserName,
                Password = registrationRequestionDto.Password,
                Name = registrationRequestionDto.Name,
                Role = registrationRequestionDto.Role
            };

            await _db.LocalUser.AddAsync(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
