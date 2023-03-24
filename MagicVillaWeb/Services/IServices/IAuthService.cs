using MagicVillaWeb.Models.Data;

namespace MagicVillaWeb.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto loginRequestDto);
        Task<T> RegisterAsync<T>(RegistrationRequestionDto registrationRequestionDto);

    }
}
