using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolovinskyAPI.Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GolovinskyAPI.Controllers
{
    /// <summary>
    /// AudioController
    /// </summary>
    [Route("api/Audio")]
    [DisableRequestSizeLimit]
    [DisableCors]
    public class AudioController : Controller
    {
        private readonly IRepository _rep; 
        public AudioController(IRepository rep)
        {
            _rep = rep;
        }
    }
}
