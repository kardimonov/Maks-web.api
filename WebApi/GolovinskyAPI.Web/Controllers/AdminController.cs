using AutoMapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Infrastructure;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models;
using GolovinskyAPI.Logic.Models.Admin;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с админкой
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [EnableCors]
    [ApiController]
    //[Route("api/Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;
        private readonly IAdminRepository repo;
        private readonly IAdminService service;
        private readonly IOptions<AuthServiceModel> _options;
        private readonly IMapper mapper;

        public AdminController(
            IAuthHandler authHandler,
            IAdminRepository repository,
            IAdminService serv,
            IOptions<AuthServiceModel> options,
            IMapper map
            )
        {
            _authHandler = authHandler;
            repo = repository;
            service = serv;
            _options = options;
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
            // var admin = service.CheckWebPasswordAdmin(loginModel, model.UserName);

            // после обновления до 5.0 метод выше включить, а код ниже удалить
            var admin = repo.CheckWebPasswordAdmin(loginModel);

            var now = DateTime.UtcNow;
            var identity = _authHandler.GetIdentity(model.UserName , admin.Cust_ID, admin.Role);
            var audience = Request.GetDisplayUrl();

            var jwt = new JwtSecurityToken(
             issuer: _options.Value.Issuer,
             audience: audience,
             notBefore: now,
             claims: identity.Claims,
             expires: now.AddMonths(1),
             signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_options), SecurityAlgorithms.HmacSha256));

            var endcodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            admin.accessToken = endcodedJwt;
            // после обновления до 5.0 код выше удалить

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