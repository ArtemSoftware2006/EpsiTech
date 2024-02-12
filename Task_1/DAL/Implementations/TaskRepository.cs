using Microsoft.EntityFrameworkCore;
using Task_1.DAL.Interfaces;

namespace Task_1.DAL.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;
        public TaskRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entity.Task> AddAsync(Domain.Entity.Task model)
        {
            await _dbContext.AddAsync(model);
            return model;
        }

        public async Task DeleteAsync(Domain.Entity.Task model)
        {
            _dbContext.Tasks.Remove(model);
        }

        public async Task<DbSet<Domain.Entity.Task>> GetAllAsync()
        {
            // Можно возвращать DbSet 
            return _dbContext.Tasks;
        }

        public async Task<Domain.Entity.Task> GetAsync(int id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(Domain.Entity.Task model)
        {
            _dbContext.Tasks.Update(model);
        }
    }
}