using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Repo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public class VillaNumberRepostory :Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepostory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumber.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
