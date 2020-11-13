using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Interfaces;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Изменение пароля
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [Route("api/password")]
    [EnableCors]
    //[ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly IEmailService service;
        //private readonly ISms_aero _sms_Aero;

        public PasswordController(IRepository repository, IEmailService serv)//, ISms_aero sms_Aero)
        {
            repo = repository;
            service = serv;
           // _sms_Aero = sms_Aero;
        }
        // GET: api/password
        /// <summary>
        /// Восстановление пароля и отправка его на email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PasswordRecoveryInput model)
        {
            var res = repo.RecoveryPassword(model);
            if (string.IsNullOrEmpty(res[0]))
            {
                return Ok(new
                {
                    Message = res[2],
                    Founded = false
                });
            }
            else
            {
                await service.SendEmailAsync(res[0], "Востановление пароля Головинский", res[1]);
                return Ok(new
                {
                    Message = res[2],
                    Founded = true
                });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] PasswordRecoveryInput model)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    var res = repo.RecoveryPassword(model);
        //    if (String.IsNullOrEmpty(res[0]))
        //    {
        //        return Ok(new 
        //        {
        //            Message = res[2],
        //            Founded = false
        //        });
        //    }
        //    else
        //    if (res[0].Contains("@"))
        //    {
        //        EmailService emailService = new EmailService();
        //        await emailService.SendEmailAsync(res[0], "Востановление пароля Головинский", res[1]);
        //        return Ok(new {
        //            Message = res[2],
        //            Founded = true
        //        });
        //    }
        //    else 
        //    {
        //        if (res[0].StartsWith("+")) res[0] = res[0].Remove(0, 1);
        //        await _sms_Aero.Send(res[0], res[1]);
        //        return Ok(new
        //        {
        //            Message = res[2],
        //            Founded = true
        //        });
        //    }
        //}
    }
}