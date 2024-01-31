using Microsoft.AspNetCore.Mvc;
using Task_1.Domain.Exceptions;
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
            try
            {
                Domain.Entity.Task task = await _taskService.GetAsync(id);

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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
                List<Domain.Entity.Task> tasks = await _taskService.GetAllAsync();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
               return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateViewModel model)
        {
            try
            {
                Domain.Entity.Task task = await _taskService.CreateAsync(model);

                if (task == null)
                {
                    return BadRequest("task not created");
                }

                return CreatedAtAction(nameof(Create), task);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskUpdateViewModel model) 
        {
            try
            {
                bool result = await _taskService.UpdateAsync(model);

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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}