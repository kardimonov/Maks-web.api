using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolovinskyAPI.Web.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService service;

        public GalleryController(IGalleryService serv)
        {
            service = serv;
        }

        /// <summary>
        /// Отобразить изображение?
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/SearchPicture
        [HttpPost]
        [Route("Gallery")]
        public IActionResult Post([FromBody] SearchPictureInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = service.SearchPicture(model);
            return Ok(response);
        }

        /// <summary>
        /// Пагинация
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/GalleryPagination 
        [HttpPost]
        [Route("GalleryPagination")]
        public IActionResult Post([FromBody] SearchPictureInputModel model, 
                           int itemsPerPage = 1, int currentPage = 1, int sort = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }            
            var response = service.SearchPicture(model, itemsPerPage, currentPage, sort);            
            return Ok(response);
        }

        [HttpPost]
        [Route("AllGalleryPagination")]
        public IActionResult AllGalleryPagination([FromBody] SearchAllPictureInputModel model, 
                           int itemsPerPage = 1, int currentPage = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }                
            var response = service.SearchAllPictures(model, itemsPerPage, currentPage);            
            return Ok(response);
        }
    }
}