using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolovinskyAPI.Infrastructure;
using GolovinskyAPI.Models;
using GolovinskyAPI.Models.ViewModels.Gallery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolovinskyAPI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [DisableCors]
    public class GalleryController : Controller
    {
        IRepository repo;
        public GalleryController(IRepository r)
        {
            repo = r;
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
                return BadRequest();
            return Ok(repo.SearchPicture(model));
        }

        /// <summary>
        /// Пагинация
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/GalleryPagination 
        [HttpPost]
        [Route("GalleryPagination")]
        public IActionResult Post([FromBody] SearchPictureInputModel model, int itemsPerPage = 1, 
                                    int currentPage = 1, int sort = 1)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var images = repo.SearchPicture(model);
            int totalItems = images.Count();
            images = images
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            var response = new GaleryViewModel
            {
                Images = images,
                TotalItems = totalItems

            };
            return Ok(response);
        }

        [HttpPost]
        [Route("AllGalleryPagination")]
        public IActionResult AllGalleryPagination([FromBody] SearchAllPictureInputModel model, int itemsPerPage = 1, int currentPage = 1)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var images = repo.SearchAllPictures(model);
            int totalItems = images.Count();
            images = images
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .OrderBy(i => i.CreatedAt)
                .ToList();

            var response = new GaleryViewModel
            {
                Images = images,
                TotalItems = totalItems

            };
            return Ok(response);
        }
    }
}
