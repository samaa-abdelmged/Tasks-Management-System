using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks_Management_System.Application.DTOs.Task;
using Tasks_Management_System.Application.Helpers;
using Tasks_Management_System.Application.InterfacesServices;

namespace Tasks_Management_System.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _service.CreateTaskAsync(userId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTaskDto dto)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _service.UpdateTaskAsync(userId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _service.DeleteTaskAsync(userId, id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyTasks()
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _service.GetMyTasksAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("section/{sectionId}")]
        public async Task<IActionResult> GetSectionTasks(int sectionId)
        {
            var result = await _service.GetSectionTasksAsync(sectionId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("shared")]
        public async Task<IActionResult> GetSharedTasks()
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _service.GetSharedTasksAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("share")]
        public async Task<IActionResult> Share(ShareTaskDto dto)
        {
            int ownerId = UserHelper.GetUserId(User);
            var result = await _service.ShareTaskAsync(ownerId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}