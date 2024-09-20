using MagicVilla_villaAPI.Data;
using MagicVilla_villaAPI.Models;
using MagicVilla_villaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_villaAPI.Repository
{
    public class VillaRepository : Repository<Villa>  ,IVillaRepository
    {
        private readonly ApplicationDbContext context;

        public VillaRepository(ApplicationDbContext context) : base(context) 
        {
            this.context = context;
        }

        //public async Task CreateAsync(Villa entity)
        //{
        //    await context.Villas.AddAsync(entity);
        //    await SaveAsync();
        //}

        //public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        //{
        //    IQueryable<Villa> query = context.Villas;
        //    if (!tracked)
        //    {
        //        query = query.AsNoTracking();
        //    }
            
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.FirstOrDefaultAsync();
        //}

        //public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        //{
        //    IQueryable<Villa> query = context.Villas;
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.ToListAsync();
        //}

        //public async Task RemoveAsync(Villa entity)
        //{
        //    context.Villas.Remove(entity);
        //    await SaveAsync();
        //}

        //public async Task SaveAsync()
        //{
        //    await context.SaveChangesAsync();   
        //}

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            context.Villas.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
