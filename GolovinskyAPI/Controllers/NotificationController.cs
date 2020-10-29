using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Models.ViewModels.Notification;
using GolovinskyAPI.Infrastructure;

namespace GolovinskyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Notification")]
    [DisableCors]
    public class NotificationController : Controller
    {
        private readonly IRepository _repository;

        public NotificationController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NotificationViewModel model)
        {
            var res = await _repository.RequestGoodsMark(model);
            return Ok(res);
        }
    }
}
