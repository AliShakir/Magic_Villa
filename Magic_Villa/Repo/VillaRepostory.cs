using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Repo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa.Repository
{
    public class VillaRepostory :Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepostory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
