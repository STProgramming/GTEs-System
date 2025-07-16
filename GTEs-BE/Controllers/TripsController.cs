using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GTEs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _service;

        public TripsController(ITripsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetTripsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var trip = await _service.GetTripAsync(id);
            return trip == null ? NotFound() : Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViaggioInputModel model)
        {
            var trip = await _service.CreateTripAsync(model);
            return CreatedAtAction(nameof(Get), new { id = trip.Id }, trip);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ViaggioInputModel updated, Guid idViaggio)
        {
            var trip = await _service.UpdateTripAsync(idViaggio, updated);
            return trip == null ? NotFound() : Ok(trip);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _service.DeleteTripAsync(id) ? NoContent() : NotFound();
        }

        [HttpPost("{id}/stops")]
        public async Task<IActionResult> AddStop(Guid id, [FromBody] Fermata stop)
        {
            return await _service.AddStopAsync(id, stop) ? Ok() : NotFound();
        }

        [HttpDelete("{tripId}/stops/{stopId}")]
        public async Task<IActionResult> RemoveStop(Guid tripId, Guid stopId)
        {
            return await _service.RemoveStopAsync(tripId, stopId) ? NoContent() : NotFound();
        }
    }
}
