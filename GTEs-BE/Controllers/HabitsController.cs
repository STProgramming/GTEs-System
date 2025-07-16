using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GTEs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        public IHabitsService _habitsService { get; set; }
        public HabitsController(IHabitsService habitsService) 
        {
            _habitsService = habitsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Abitudine>>> GetAll()
        {
            return Ok(await _habitsService.GetHabitsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Abitudine>> Get(string id)
        {
            var abitudine = await _habitsService.GetHabitAsync(id);
            return abitudine == null ? NotFound() : Ok(abitudine);
        }

        [HttpPost]
        public async Task<ActionResult<Abitudine>> Create([FromBody] AbitudineInputModel input)
        {
            var created = await _habitsService.CreateHabitAsync(input);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Abitudine>> Update(Guid id, [FromBody] AbitudineInputModel input)
        {
            var updated = await _habitsService.UpdateHabitAsync(id, input);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _habitsService.DeleteHabitAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
