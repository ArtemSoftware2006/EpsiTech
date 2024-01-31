using Task_1.Domain.ViewModels;

namespace Task_1.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Domain.Entity.Task> CreateAsync(TaskCreateViewModel model);
        Task<List<Domain.Entity.Task>> GetAllAsync();
        Task<Domain.Entity.Task> GetAsync(int id);
        Task<bool> UpdateAsync(TaskUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

    }
}