using System;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Images;
using GolovinskyAPI.Logic.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с картинками
    /// </summary>
    /// <returns></returns>
    //[Produces("application/json")]
    [Route("api/Img")]
    [DisableRequestSizeLimit]
    [EnableCors]
    //[ApiController]
    public class ImgController : ControllerBase
    {
        private readonly IPictureRepository repo;
        private readonly IPictureService service;

        public ImgController(IPictureRepository repository, IPictureService serv)
        {            
            repo = repository;
            service = serv;
        }

        /// <summary>
        /// Отобразить картинку
        /// </summary>
        // GET: api/Img/5
        [HttpGet]
        public IActionResult Get(string AppCode, string ImgFileName)
        {
            var res = repo.GetImageMobile(AppCode, ImgFileName);
            if (res != null)
            {
                //return Ok("data:image/jpeg;base64," + Convert.ToBase64String(res));
                return File(res, "image/jpeg;base64");
            }
            return BadRequest();
        }

        /// <summary>
        /// Добавление картинки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Img
        [HttpPost]
        public IActionResult Post([FromBody] SearchPictureInfoInputModel model)
        {
            if (ModelState.IsValid)
            {
                var res = service.SearchPictureInfo(model);
                if (res != null)
                {
                    return Ok(res);
                }
            }
            return BadRequest();
        }

        //[HttpPost("ImagesInfo")]
        //public IActionResult ImagesInfo([FromBody] SearchPicturesInfoInputModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        List<SearchPictureInfoOutputModel> res = new List<SearchPictureInfoOutputModel>();
        //        var prcIds = repo.SearchPicture(new SearchPictureInputModel
        //        {
        //            ID = model.CategoryId
        //        });
        //        foreach (var item in prcIds)
        //        {
        //            var respart = repo.SearchPictureInfo(new SearchPictureInfoInputModel { AppCode = model.AppCode,Cust_ID = model.Cust_ID, Prc_ID = item.Prc_ID});
        //            res.Add(respart);
        //        }
        //        if (res != null)
        //        {
        //            return Ok(res);
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}

        /// <summary>
        /// Изменение картинки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/api/img/upload")]
        //[Authorize]
        public IActionResult Upload([FromForm] NewUploadImageInput model)
        {
            Console.WriteLine();
            var ts = DateTime.Now;
            Console.WriteLine("upload request started" + ts.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest("параметры запроса некорректные");
            }
            var res = service.UploadPicture(model);
            Console.WriteLine("upload request ended " + (DateTime.Now - ts).TotalMilliseconds);
            return Ok(new { result = res });
        }

        /// <summary>
        /// Удаление картинки
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("/api/img/")]
       // [Authorize]
        public IActionResult Delete([FromBody] SearchPictureInfoInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("параметры запроса некорректные");
            }
            var res = repo.DeleteMainPicture(model);
            return Ok(new { result = res });
        }        
    }
}