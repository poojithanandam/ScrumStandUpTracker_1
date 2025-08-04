using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumStandUpTracker_1.DTOs;
using ScrumStandUpTracker_1.Service;

namespace ScrumStandUpTracker_1.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class StatusFormController : ControllerBase
    {
        public readonly StatusFormService _service;
        public StatusFormController(StatusFormService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusFormDTO>>> GetAllStatuses()
        {
            var statuses = await _service.GetAllStatus();
            return Ok(statuses);
        }
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<StatusFormDTO>>> GetStatusByDate(DateTime date)
        {
            var status =await  _service.GetStatusByDate(date);
            return Ok(status);
        } 
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<StatusFormDTO>>> GetStatusByDeveloperName(string name)
        {
            var status = await _service.GetStatusByDeveloperName(name);
            return Ok(status);
        }
        [HttpPost]
        public async Task<ActionResult> AddStatus(StatusFormDTO statusdto)
        {
            await _service.AddStatus(statusdto);
            return CreatedAtAction(nameof(GetAllStatuses), null);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStatus(int id, StatusFormDTO statusdto)
        {
            if (id != statusdto.StatusFormId)
            {
                return BadRequest("Id not matched");
            }
            await _service.UpdateStatus(statusdto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)    
        {
            await _service.DeleteStatus(id);
            return NoContent();
        }
    }
}
