using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Logic.Infrastructure;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models;

namespace GolovinskyAPI.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [Route("api/Authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly ICustomizeService service;
        private readonly IOptions<AuthServiceModel> _options;
        private readonly IAuthHandler _authHandler;

        public AuthorizationController(            
            IAuthRepository repository,
            ICustomizeService serv,
            IOptions<AuthServiceModel> options,
            IAuthHandler authHandler
            )
        {
            repo = repository;
            service = serv;
            _options = options;
            _authHandler = authHandler;
        }

        // POST: api/Authorization
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Некорректные данные в запросе");
            }

            var userId = repo.CheckWebPassword(model);
            var identity = _authHandler.GetIdentity(model.UserName, userId, "customer");

            if (identity == null)
            {
                return NotFound(new { result = false, message = "Не верный логин и пароль" });
            }
            var now = DateTime.UtcNow;

            var custInfo = repo.GetCustomerFIO(Convert.ToInt32(identity.Claims.ElementAt(2).Value));
            string fio = null;
            if (custInfo != null)
            {
                fio = custInfo.FIO;
            }
            
            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: Request.GetDisplayUrl(),
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMonths(1),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(_options), SecurityAlgorithms.HmacSha256));
            
            var endcodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new LoginSuccessModel
            {
                AccessToken = endcodedJwt,
                UserName = identity.Name,
                FIO = fio,
                Phone = custInfo.Phone,
                Email = custInfo.E_mail,
                Skype = custInfo.Skype,
                WhatsApp = custInfo.Whatsapp,
                Role = identity.Claims.ElementAt(1).Value,
                UserId = identity.Claims.ElementAt(2).Value,
            };

            var mainImage = service.GetMainImage();
            response.MainImage = mainImage;

            return Ok(response);
        }
                
        // PUT: api/Authorization
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]RegisterInputModel model)
        {
            RegisterOutputModel regOutputModel = repo.AddWebCustomerCompany(model);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }                
            if (regOutputModel.Cust_ID == 999999 && regOutputModel.AuthCode.Length == 0)
            {
                regOutputModel.Result = false;
                return Ok(regOutputModel);
            }
            return Ok(regOutputModel);
        }

        // PUT: api/UpdateUser
        /// <summary>
        /// Обновление персональных данных
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            var response = await repo.UpdateUser(model);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}