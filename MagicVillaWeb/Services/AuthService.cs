using MagicVillaWeb.Models;
using MagicVillaWeb.Models.Data;
using MagicVillaWeb.Services.IServices;
using MaginVillaUtility;

namespace MagicVillaWeb.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        string villaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestionDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "api/UsersAuth/register"
            });
        }
    }
}
