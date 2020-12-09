using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Notification;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IRepository repo;

        public NotificationController(IRepository repository)
        {
            repo = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotificationViewModel model)
        {
            var res = await repo.RequestGoodsMark(model);
            return Ok(res);
        }
    }
}