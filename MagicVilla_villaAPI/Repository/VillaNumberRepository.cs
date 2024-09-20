using MagicVilla_villaAPI.Data;
using MagicVilla_villaAPI.Models;
using MagicVilla_villaAPI.Repository.IRepository;

namespace MagicVilla_villaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext context;

        public VillaNumberRepository(ApplicationDbContext context) : base(context) 
        {
            this.context = context;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            context.VillaNumbers.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
