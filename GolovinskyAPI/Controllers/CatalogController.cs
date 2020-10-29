using System;
using GolovinskyAPI.Models.Catalog;
using GolovinskyAPI.Models.ViewModels.Catalog;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using GolovinskyAPI.Infrastructure.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections;
using System.Linq;

namespace GolovinskyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    [DisableCors]
    public class CatalogController : Controller
    {
        private readonly ICatalogRepository _rep;

        public CatalogController(ICatalogRepository rep)
        {
            _rep = rep;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateCatalogViewModel model)
        {
            var newCatalog = new Catalog()
            {
                Id = model.Id,
                Name = model.Name,
                ImgName = model.ImgName,
                CustIdMain = model.CustIdMain
            };

            var res = _rep.Create(newCatalog);

            return Ok(res);
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditCatalogViewModel model)
        {
            var newCatalog = new Catalog()
            {
                Id = model.Id,
                Name = model.Name,
                ImgName = model.ImgName,
                CustIdMain = model.CustIdMain
            };

            var res = _rep.Update(newCatalog);

            return Ok(res);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteCatalogViewModel model)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(model.AccessToken);
            int userId = Convert.ToInt32(token.Claims.ToList()[2].Value);

            if(userId != model.CustIdMain)
            {
                return Forbid();
            }

            var newCatalog = new Catalog()
            {
                Id = model.Id,
                CustIdMain = model.CustIdMain
            };

            var res = _rep.Delete(newCatalog);

            return Ok(res);
        }
    }
}
