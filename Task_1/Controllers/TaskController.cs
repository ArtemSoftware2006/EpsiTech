using Microsoft.AspNetCore.Mvc;
using Task_1.Domain.ViewModels;
using Task_1.Services.Interfaces;

namespace Task_1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            Domain.Entity.Task task = await _taskService.GetAsync(id);

            if (task == null)
            {
                return BadRequest("task not found");
            }

            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            List<Domain.Entity.Task> tasks = await _taskService.GetAllAsync();

            if (tasks == null)
            {
                return StatusCode(500, "Internal server error");
            }

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateViewModel model)
        {
            Domain.Entity.Task task = await _taskService.CreateAsync(model);

            if (task == null)
            {
                return BadRequest("task not created");
            }

            return CreatedAtAction(nameof(Create), task);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskUpdateViewModel model) 
        {
            bool result = await _taskService.UpdateAsync(model);

            if (!result)
            {
                return BadRequest("task not updated");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _taskService.DeleteAsync(id);

            if (!result)
            {
                return BadRequest("task not deleted");
            }

            return NoContent();
        }
    }
}