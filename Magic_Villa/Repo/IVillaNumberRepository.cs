using Magic_Villa.Models;
using System.Linq.Expressions;

namespace Magic_Villa.Repo
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {        
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
