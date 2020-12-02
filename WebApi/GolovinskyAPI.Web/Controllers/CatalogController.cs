using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using GolovinskyAPI.Data.Models.Catalog;
using GolovinskyAPI.Data.Interfaces;
using AutoMapper;
using GolovinskyAPI.Logic.Models.Catalog;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    [EnableCors]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository repo;
        private readonly IMapper mapper;

        public CatalogController(ICatalogRepository repository, IMapper map)
        {
            repo = repository;
            mapper = map;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCatalogViewModel model)
        {
            var newCatalog = mapper.Map<Catalog>(model);
            var res = repo.Create(newCatalog);

            return Ok(res);
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditCatalogViewModel model)
        {
            var newCatalog = mapper.Map<Catalog>(model);
            var res = repo.Update(newCatalog);

            return Ok(res);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteCatalogViewModel model)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(model.AccessToken);
            var userId = Convert.ToInt32(token.Claims.ToList()[2].Value);

            if (userId != model.CustIdMain)
            {
                return Forbid();
            }
            var newCatalog = mapper.Map<Catalog>(model);
            var res = repo.Delete(newCatalog);

            return Ok(res);
        }
    }
}