using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using GolovinskyAPI.Data.Models.Mobile;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Categories;
using GolovinskyAPI.Logic.Infrastructure;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Load")]
    [EnableCors]
    //[ApiController]
    public class LoadController : ControllerBase
    {
        private readonly ILoadRepository repo;

        public LoadController(ILoadRepository repository)
        {
            repo = repository;
        }

        /// <summary>
        /// Переход в конкретный магазин
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Load/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = repo.GetCustId(id);
            return Ok(res);
        }

        // POST: api/Load
        [HttpPost]
        public IActionResult Post([FromBody] SearchAvitoPictureInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }                
            //string route = "http://" + model.Id.ToString() + "." + "golowinskiy.bostil.ru/";
            //Console.WriteLine(route);
            //return Redirect(route);
            return Ok(repo.SearchAvitoPicture(model));
        }

        /// <summary>
        /// Получение категорий магазина
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //POST: api/categories
        [HttpPost("/api/categories/")]
        public IActionResult GetCategories([FromBody] CategoriesInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }                

            var catRecursion = new CategoryRecursion();
            var outputCategories = repo.GetCategoryItems(model);   
            
            return Ok(catRecursion.GenerateCategories(outputCategories));
        }

        [HttpPost("/api/getMobileDB/")]
        public IActionResult GetMobileDB([FromBody] GetMobileDbModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(repo.GetMobileDB(model));
        }

        [HttpPut("/api/addInetMobileOrder/")]
        public IActionResult AddInetMobileOrder([FromBody] AddInetMobileOrdeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(repo.AddInetMobileOrder(model));
        }
    }
}