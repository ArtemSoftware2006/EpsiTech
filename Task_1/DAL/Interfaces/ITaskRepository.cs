using Task_1.Domain.ViewModels;

namespace Task_1.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<Domain.Entity.Task> AddAsync(Domain.Entity.Task model);
        Task<Domain.Entity.Task> GetAsync(int id);
        Task<List<Domain.Entity.Task>> GetAllAsync();
        Task UpdateAsync(Domain.Entity.Task model); 
        Task DeleteAsync(Domain.Entity.Task model);
        Task<bool> SaveChangesAsync();
    }
}