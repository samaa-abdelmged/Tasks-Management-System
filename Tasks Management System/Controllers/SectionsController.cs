using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks_Management_System.Application.Helpers;
using Tasks_Management_System.Application.DTOs.Section;
using Tasks_Management_System.Application.InterfacesServices;

namespace Tasks_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionDto dto)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _sectionService.CreateSectionAsync(userId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMySections()
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _sectionService.GetMySectionsAsync(userId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("shared")]
        public async Task<IActionResult> GetSharedSections()
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _sectionService.GetSharedSectionsAsync(userId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSection([FromBody] UpdateSectionDto dto)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _sectionService.UpdateSectionAsync(userId, dto);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{sectionId}")]
        public async Task<IActionResult> DeleteSection(int sectionId)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _sectionService.DeleteSectionAsync(userId, sectionId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareSection([FromBody] ShareSectionDto dto)
        {
            int userId = UserHelper.GetUserId(User);

            var result = await _sectionService.ShareSectionAsync(userId, dto);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}