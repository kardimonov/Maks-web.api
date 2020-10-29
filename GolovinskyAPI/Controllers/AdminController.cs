using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolovinskyAPI.Infrastructure.Administration;
using GolovinskyAPI.Models;
using GolovinskyAPI.Models.ViewModels.Products;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Models.ViewModels.Admin;
using GolovinskyAPI.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using GolovinskyAPI.Services;
using Microsoft.AspNetCore.Http.Extensions;
using GolovinskyAPI.Infrastructure.Interfaces;
using GolovinskyAPI.Models.ViewModels.Gallery;

namespace GolovinskyAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с админкой
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [DisableCors]
    //[Route("api/Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;
        private readonly ITemplateRepository _tempRep;
        private readonly IRepository _rep;
        private readonly IOptions<AuthServiceModel> _options;

        public AdminController(
            IAuthHandler authHandler,
            ITemplateRepository tempRep, 
            IRepository rep, 
            IOptions<AuthServiceModel> options
            )
        {
            _authHandler = authHandler;
            _tempRep = tempRep;
            _rep = rep;
            _options = options;
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

            return Ok(_tempRep.CustomerInfoPromo(id));
        }

        [HttpPost("api/Admin/Login")]
        public IActionResult Login([FromBody] AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("параметры запроса некорректные");
            }

            var loginModel = new LoginModel()
            {
                UserName = model.UserName,
                Password = model.Password
            };

            var admin = _rep.CheckWebPasswordAdmin(loginModel);

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

            return Ok(admin);
        }

        // POST: api/Admin
        /// <summary>
        /// Загрузка txt документа в БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/Admin/Upload")]
        public IActionResult Upload([FromBody] UploadDBfromtxt model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("параметры запроса некорректные");
            }
            bool res = _tempRep.UploadDatabaseFromtxt(model);
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var res = _tempRep.SearchPictureInfo(model);
                if (res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
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
                return Ok(_tempRep.SearchProduct(model));

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("api/Admin/Gallery")]
        public IActionResult Post([FromBody] AdminGalleryViewModel model, int itemsPerPage = 1, int currentPage = 1)
        {
            if (ModelState.IsValid)
            {
                var dto = new AdminPictureInfo
                {
                    Cust_ID = model.Cust_ID,
                    StartDate = model.StartDate,
                    FinishDate = model.FinishDate,
                    SearchDescr = model.Search

                };

                var images =_rep.SearchAllAdminPictures(dto);
                int totalItems = images.Count();
                images = images
                    .Skip((currentPage - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();

                var response = new GaleryViewModel
                {
                    Images = images,
                    TotalItems = totalItems

                };
                return Ok(response);

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("api/Admin/ChangeStatus")]
        public IActionResult Put([FromBody] ChangeStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _rep.SetAdvertStatus(model);

                if (response)
                    return Ok();
                else
                    return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}