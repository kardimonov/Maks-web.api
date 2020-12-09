using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Admin;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models.Admin;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с админкой
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [ApiController]
    //[Route("api/Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository repo;
        private readonly IAdminService service;
        private readonly IMapper mapper;

        public AdminController(
            IAdminRepository repository,
            IAdminService serv,
            IMapper map
            )
        {
            repo = repository;
            service = serv;
            mapper = map;
        }

        /// <summary>
        /// Отображение данных клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("api/Admin/UserInfo")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("id пользователя не задан");
            }
            return Ok(repo.CustomerInfoPromo(id));
        }

        [HttpPost("api/Admin/Login")]
        public IActionResult Login([FromBody] AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("параметры запроса некорректные");
            }

            var loginModel = mapper.Map<LoginModel>(model);
            var audience = Request.GetDisplayUrl();
            var admin = service.CheckWebPasswordAdmin(loginModel, model.UserName, audience);

            return Ok(admin);
        }

        // POST: api/Admin
        /// <summary>
        /// Загрузка txt документа в БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/Admin/Upload")]
        public IActionResult Upload([FromBody] UploadDbFromTxt model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("параметры запроса некорректные");
            }
            var res = repo.UploadDatabaseFromTxt(model);
            return Ok(new { result = res });
        }

        // POST: api/Admin
        /// <summary>
        /// Просмотр информации о товаре
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/Admin/GetInfo")]
        public IActionResult GetInfo([FromBody] SearchPictureInfoInputModel model)
        {
            if (ModelState.IsValid)
            {
                var res = repo.SearchPictureInfo(model);
                if (res != null)
                {
                    return Ok(res);
                }
            }
            return BadRequest();
        }

        // POST: api/Admin
        /// <summary>
        /// поиск товара
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/Admin/Search")]
        public IActionResult Search([FromBody] SearchPictureInputModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(repo.SearchProduct(model));
            }
            return BadRequest();
        }

        [HttpPost("api/Admin/Gallery")]
        public IActionResult Post([FromBody] AdminGalleryViewModel model, int itemsPerPage = 1, int currentPage = 1)
        {
            if (ModelState.IsValid)
            {
                var dto = mapper.Map<AdminPictureInfo>(model);
                var response = service.SearchAllAdminPictures(dto, itemsPerPage, currentPage);                
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("api/Admin/ChangeStatus")]
        public IActionResult Put([FromBody] ChangeStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = repo.SetAdvertStatus(model);
                if (response)
                {
                    return Ok();
                }
            }
            return BadRequest();            
        }
    }
}