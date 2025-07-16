using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace GTEs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingsController : ControllerBase
    {
        private readonly ISystemSettingsService _service;

        public SystemSettingsController(ISystemSettingsService service)
        {
            _service = service;
        }

        [HttpGet("/contact")]
        public async Task<IList<Contatto>> GetAsync()
        {
            return await _service.GetContactsAsync();
        }

        [HttpGet("/contact/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string idContactRequest)
        {
            if (string.IsNullOrWhiteSpace(idContactRequest)) return BadRequest();
            try
            {
                return Ok(await _service.GetContactAsync(idContactRequest));
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpPost("/contact")]
        public async Task<IActionResult> CreateAsync([FromBody] ContattoInputModel inputModel)
        {
            if(!ModelState.IsValid) return BadRequest();            
            await _service.CreateContactAsync(inputModel);
            return NoContent();
        }

        [HttpDelete("/contact/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string IdContact)
        {
            if (string.IsNullOrWhiteSpace(IdContact)) return BadRequest();
            try
            {
                await _service.DeleteContactAsync(IdContact);
                return NoContent();
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(OperationCanceledException ex)
            {
                return Conflict();
            }
        }
    }
}
