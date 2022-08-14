using GoFinStrategy.Domain.Entites;
using GoFinStrategy.Domain.Interfaces.Repositories;
using GoFinStrategy.Infrastructure.Data.Postgresql.Context;
using Microsoft.EntityFrameworkCore;

namespace GoFinStrategy.Infrastructure.Data.Postgresql.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Category>();
        }

        public async Task<Guid> Add(Category category)
        {

            await _dbSet.AddAsync(category);
            return category.Id;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbSet.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Remove(Guid id)
        {
            var category = await GetById(id);

            if (category == null)
                return false;

            _dbSet.Remove(category);
            return true;
        }

        public async Task<Guid> Update(Guid id, Category category)
        {
            if (await GetById(id) == null || id != category.Id)
                return Guid.Empty;

            _dbSet.Update(category);

            return category.Id;
        }
    }
}
