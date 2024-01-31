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
                Domain.Entity.Task task = new Domain.Entity.Task()
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
                Domain.Entity.Task task = await _taskRepository.GetAsync(id);

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
                List<Domain.Entity.Task> tasks = await _taskRepository.GetAllAsync();

                return tasks;
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
                Domain.Entity.Task task = await _taskRepository.GetAsync(id);

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TaskUpdateViewModel model)
        {
            try
            {
                Domain.Entity.Task task = await _taskRepository.GetAsync(model.Id);

                if (task == null)
                {
                    _logger.LogWarning($"task not found (id = {model.Id})");
                    throw new EntityNotFoundException($"task not found (id = {model.Id})");
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