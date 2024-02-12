using HealthcareApp.Models.DataModels;
using HealthcareApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Repository.Implementation
{
    // generic CRUD operation implementation for DataModel classes
    public abstract class CrudRepository<T> : ICrudRepository<T> where T : BaseDataModel
    {
        protected readonly HealthcareDbContext _context;

        public CrudRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async virtual Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async virtual Task<T?> Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity is not null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async virtual Task<bool> Exists(Guid id)
        {
            return (await _context.Set<T>().AnyAsync(e => e.Id == id));
        }

        public async virtual Task<List<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async virtual Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async virtual Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async virtual Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
