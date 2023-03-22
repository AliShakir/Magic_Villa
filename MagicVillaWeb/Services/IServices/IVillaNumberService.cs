using MagicVillaWeb.Models.Data;

namespace MagicVillaWeb.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNoCreateDto dto);
        Task<T> UpdateAsync<T>(VillaNoUpdateDto dto);
        Task<T> DeleteAsync<T>(int id);
       
    }
}
