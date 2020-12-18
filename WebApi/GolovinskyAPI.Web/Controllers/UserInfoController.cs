using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GolovinskyAPI.Data.Interfaces;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/UserInfo")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IRepository repo;

        public UserInfoController(IRepository repository)
        {
            repo = repository;
        }

        /// <summary>
        /// Отображение данных клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/UserInfo/5
        [HttpGet("{id}", Name = "UserInfo")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("id пользователя не задан");
            }
            return Ok(repo.CustomerInfoPromo(id));
        }
    }
}