﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Localization")]
    [EnableCors]
    //[ApiController]
    public class LocalizationController : ControllerBase
    {
        // POST: api/Localization
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}