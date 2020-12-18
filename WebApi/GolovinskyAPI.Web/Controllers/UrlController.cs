using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Data.Models.ShopInfo;
using System.Threading.Tasks;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IShopRepository repo;
        private readonly ICustomizeService service;

        public UrlController(IShopRepository repository, ICustomizeService serv)
        {
            repo = repository;
            service = serv;
        }

        /// <summary>
        /// Отображение информации о магазине
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("api/shopinfo/{url}")]
        public IActionResult Get(string url)
        {
            var mainImage = service.GetMainImage();
            var accountMainImage = service.GetMainImageUserAccount();
            var res = repo.GetSubDomain(url);
            if (res == null)
            {
                return NotFound(new { 
                    Message = $"Извините, магазин {url}.головинский.рф не найден", 
                    MainPicture = $"/mainimages/{mainImage}",
                    MainPictureAccountUser = $"/accountImages/{accountMainImage}",
                Status = false });
            }
            res.MainPicture = $"/mainimages/{mainImage}";
            res.MainPictureAccountUser = $"/accountImages/{accountMainImage}";

            return Ok(new {
                cust_id = res.Cust_id, 
                mainImage = res.MainPicture,
                mainPictureAccountUser = res.MainPictureAccountUser,
                dz = res.DZ,
                email = res.E_mail,                
                phone = res.Phone,
                shortDescr = res.ShortDescr,
                addr = res.Addr,
                welcome = res.Welcome,
                manual = res.Manual
            });
        }

        /// <summary>
        /// Отображение реквизитов магазина
        /// </summary>
        /// <param name="id"></param>
        // GET: api/ShopDetails/19139
        [HttpGet("/api/ShopDetails/{id}")]
        public async Task<IActionResult> GetShopDetails(int? id)
        {
            if (id == null)
            {
                return BadRequest("Shop id is not defined");
            }
            var result = await repo.GetShopDetailsAsync((int)id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Редактирование реквизитов магазина
        /// </summary>
        /// <param name="model"></param>
        // PUT: api/ShopDetails/
        [HttpPut("/api/ShopDetails/")]
        public async Task<IActionResult> UpdateShopDetails([FromBody] ShopDetailsPut model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await repo.UpdateShopDetailsAsync(model));
        }
    }
}