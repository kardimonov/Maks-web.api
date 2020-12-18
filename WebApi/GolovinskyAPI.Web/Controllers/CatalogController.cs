using AutoMapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Catalog;
using GolovinskyAPI.Logic.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
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
        public IActionResult Post([FromBody] CreateCatalog model)
        {
            var newCatalog = mapper.Map<Catalog>(model);
            var res = repo.Create(newCatalog);

            return Ok(res);
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditCatalog model)
        {
            var newCatalog = mapper.Map<Catalog>(model);
            var res = repo.Update(newCatalog);

            return Ok(res);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteCatalog model)
        {
            var newCatalog = mapper.Map<Catalog>(model);
            var res = repo.Delete(newCatalog);

            return Ok(res);
        }
    }
}