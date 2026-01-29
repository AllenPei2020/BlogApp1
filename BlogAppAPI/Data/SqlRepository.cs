
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogAppAPI.Data
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        public SqlRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity =await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<List<T>> GetAllFilteredAsync(Expression<Func<T,bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
