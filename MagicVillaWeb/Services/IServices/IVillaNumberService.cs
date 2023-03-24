using MagicVillaWeb.Models.Data;

namespace MagicVillaWeb.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNoCreateDto dto, string token);
        Task<T> UpdateAsync<T>(VillaNoUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
       
    }
}
