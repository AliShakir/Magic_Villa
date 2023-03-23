using Magic_Villa.Models;
using Magic_Villa.Models.Data;

namespace Magic_Villa.Repo
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<LocalUser> Register(RegistrationRequestionDto registrationRequestionDto);
    }
}
