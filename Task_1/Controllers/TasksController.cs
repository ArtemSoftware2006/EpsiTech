using Microsoft.AspNetCore.Mvc;
using Task_1.Domain.Exceptions;
using Task_1.Domain.ViewModels;
using Task_1.Services.Interfaces;

namespace Task_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            try
            {
                //Использовать var
                var task = await _taskService.GetAsync(id);

                if (task == null)
                {
                    return BadRequest("task not found");
                }

                return Ok(task);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var tasks = await _taskService.GetAllAsync();

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateViewModel model)
        {   
            var task = await _taskService.CreateAsync(model);

            if (task == null)
            {
                return BadRequest("task not created");
            }

            return CreatedAtAction(nameof(Create), task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TaskUpdateViewModel model, int id) 
        {
            try
            {
                bool result = await _taskService.UpdateAsync(id, model);

                if (!result)
                {
                    return BadRequest("task not updated");
                }

                return NoContent();
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _taskService.DeleteAsync(id);

                return NoContent();   
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}