using GTEs_BE.Datas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GTEs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemStatusController : ControllerBase
    {
        public SystemStatusController() { }

        [HttpGet]
        public IActionResult GetStatus() => Ok(new
        {
            SystemStatusModel.IsUserOnBoard,
            SystemStatusModel.IsCarLocked,
            SystemStatusModel.IsUserAuthenticated,
        });

        [HttpPost("onboard")]
        public IActionResult SetOnBoard([FromBody] bool IsOnBoard)
        {
            SystemStatusModel.IsUserOnBoard = IsOnBoard;
            return NoContent();
        }

        [HttpPost("weconnect/authenticated")]
        public IActionResult SetAuthWeConnect([FromBody] bool IsAuth)
        {
            SystemStatusModel.IsUserAuthenticated = IsAuth;
            return NoContent();
        }
    }
}
