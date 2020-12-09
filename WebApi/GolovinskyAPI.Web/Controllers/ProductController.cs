using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Products;
using GolovinskyAPI.Data.Models;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Работа с товарами
    /// </summary>
    [Produces("application/json")]
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repo;

        public ProductController(IProductRepository repository)
        {
            repo = repository;
        }

        // POST: api/Product
        /// <summary>
        /// Добавление нового товара
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
       // [Authorize]
        public IActionResult Post([FromBody]NewProductInputModel model)
        {
            var ts = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("Product Post request started" + ts.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var res = repo.InsertProduct(model);
            Console.WriteLine("Product Post request ended " + (DateTime.Now - ts).TotalMilliseconds);
            return Ok(new { result = res.Result, prc_id = res.Prc_ID });
        }

        // PUT: api/Product/
        /// <summary>
        /// Изменение товара
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody] NewProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var res = repo.UpdateProduct(model);
            return Ok(new { result = res });
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromBody] DeleteProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var res = repo.DeleteProduct(model);
            return Ok(new { result = res });
        }
        
        /// <summary>
        /// Поиск продукта
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/product/search/")]
        public IActionResult Search([FromBody] SearchPictureInputModel model )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(repo.SearchProduct(model));
        }
    }
}