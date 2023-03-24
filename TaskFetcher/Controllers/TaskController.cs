using Microsoft.AspNetCore.Mvc;
using TaskFetcher.Services.Interfaces;

namespace TaskFetcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask()
        {
            return Ok(await _taskService.CreateTask());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid res))
            {
                return BadRequest();
            }

            var task = await _taskService.GetTask(Guid.Parse(id));

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
    }
}
