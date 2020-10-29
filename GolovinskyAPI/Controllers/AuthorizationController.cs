using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolovinskyAPI.Infrastructure;
using GolovinskyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using GolovinskyAPI.Services;
using Microsoft.AspNetCore.Cors;
using GolovinskyAPI.Infrastructure.Interfaces;

namespace GolovinskyAPI.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [Route("api/Authorization")]
    [DisableCors]
    public class AuthorizationController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly IOptions<AuthServiceModel> _options;
        private readonly IAuthHandler _authHandler;

        public AuthorizationController(
            IAuthHandler authHandler,
            IRepository r, 
            IOptions<AuthServiceModel> options
            )
        {
            _authHandler = authHandler;
            _options = options;
            repo = r;
        }

        // POST: api/Authorization
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
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
            if(custInfo!= null) fio = custInfo.FIO;
            
            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: GetAUDIENCE(),
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMonths(1),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_options), SecurityAlgorithms.HmacSha256));
            
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

            var customizeService = new CustomizeService();
            var mainImage = customizeService.GetMainImage();
            response.MainImage = mainImage;

            return Ok(response);
        }
        
        [NonAction]
        public string GetAUDIENCE()
        {
            var f = Request.GetDisplayUrl();
            return f;
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
                return BadRequest();
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
                return BadRequest();

            return Ok(response);
        }
    }
}
