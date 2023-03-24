using MagicVillaWeb.Models;
using MagicVillaWeb.Models.Data;
using MagicVillaWeb.Services.IServices;
using MaginVillaUtility;

namespace MagicVillaWeb.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        string villaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) 
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNoCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "api/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "api/VillaNumberAPI/id:int?id=" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "api/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "api/VillaNumberAPI/id?id=" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNoUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "api/VillaNumberAPI/id:int?id=" + dto.VillaNo,
                Token = token
            });
        }
    }
}
