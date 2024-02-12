using Microsoft.EntityFrameworkCore;
using Task_1.DAL.Interfaces;
using Task_1.Domain.Exceptions;
using Task_1.Domain.ViewModels;
using Task_1.Services.Interfaces;

namespace Task_1.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository,
            ILogger<TaskService> logger) 
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }
        public async Task<Domain.Entity.Task> CreateAsync(TaskCreateViewModel model)
        {
            try
            {
                var task = new Domain.Entity.Task()
                {
                    Name = model.Name.Trim(),
                    Text = model.Text.Trim(),
                    DatePublish = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow
                };

                task = await _taskRepository.AddAsync(task);
                await _taskRepository.SaveChangesAsync();

                return task;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "can't create new task");
                throw;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);

                if (task == null)
                {
                    _logger.LogWarning($"task not found (id = {id})");
                    throw new EntityNotFoundException($"task not found (id = {id})");
                }

                await _taskRepository.DeleteAsync(task);
                await _taskRepository.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "can't delete task");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<List<Domain.Entity.Task>> GetAllAsync()
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync();

                return await tasks.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Domain.Entity.Task> GetAsync(int id)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, TaskUpdateViewModel model)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);

                if (task == null)
                {
                    _logger.LogWarning($"task not found (id = {id})");
                    throw new EntityNotFoundException($"task not found (id = {id})");
                }
                
                task.Text = model.Text.Trim();
                task.Name = model.Name.Trim();
                task.DateUpdate = DateTime.UtcNow;

                await _taskRepository.UpdateAsync(task);
                await _taskRepository.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "can't update task");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}