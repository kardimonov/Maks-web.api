using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MobileApi.Data.Models;
using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Interfaces;
using MobileApi.Logic.Models;

namespace golowinsky_mobile.Controllers
{
    [Produces("application/json")]
    [Route("ws/carprc.svc/")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IVariaService variaService;
        private readonly IVideoService videoService;
        private readonly IBaseService baseService;
        private readonly IOrderService orderService;
        private readonly IPictureService pictureService;

        public MainController(IVariaService variaServ, 
            IVideoService videoServ, IBaseService baseServ, 
            IOrderService orderServ, IPictureService pictureServ)
        {
            variaService = variaServ;
            videoService = videoServ;
            baseService = baseServ;
            orderService = orderServ;
            pictureService = pictureServ;
        }

        /// <summary>
        /// This is xml comments. here is a method description
        /// </summary>
        /// <param name="app_code">what is app_code</param>
        /// <param name="url">what is url</param>
        /// <returns>what is returns</returns>
        [HttpPost("CheckMobileAppCodeUrl/{app_code}/{url}")]
        public ActionResult<TCheckMobileAppCodeUrl> CheckMobileAppCodeUrl(string app_code, string url)
        {
            var response = variaService.CheckMobileAppCodeUrl(app_code, url);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("UpdateMarkMobileDB/{app_code}/{code_mobile}/{tablename}")]
        public ActionResult<TUpdateMarkMobileDB> UpdateMarkMobileDB(string app_code, string code_mobile, string tablename)
        {
            var response = variaService.UpdateMarkMobileDB(app_code, code_mobile, tablename);
            return new JsonResult (response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileShowLogin/{app_code}")]
        public ActionResult<TGetMobileShowLogin> GetMobileShowLogin(string app_code)
        {
            var response = variaService.GetMobileShowLogin(app_code);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetNewTablesMobileDB/{app_code}/{code_mobile}")]
        public ActionResult<List<TGetNewTablesMobileDB>> GetNewTablesMobileDB(string app_code, string code_mobile)
        {
            var response = baseService.GetNewTablesMobileDB(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetAllTablesMobileDB/{app_code}/{code_mobile}")]
        public ActionResult<List<TGetNewTablesMobileDB>> GetAllTablesMobileDB(string app_code, string code_mobile)
        {
            var response = baseService.GetAllTablesMobileDB(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBpredpr/{app_code}/{code_mobile}")]
        public ActionResult<List<Tpredpr>> GetMobileDBpredpr(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBpredpr(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_gr/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_gr>> GetMobileDBtov_gr(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_gr(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_art/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_art>> GetMobileDBtov_art(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_art(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_img/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_img>> GetMobileDBtov_img(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_img(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_img_async/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_img>> GetMobileDBtov_img_async(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_img_async(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_img_base64_async/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_img_base64>> GetMobileDBtov_img_base64_async(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_img_base64_async(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_img_sync/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_img>> GetMobileDBtov_img_sync(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_img_sync(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_contacts/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_contacts>> GetMobileDBtov_contacts(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_contacts(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBctlg/{app_code}/{code_mobile}")]
        public ActionResult<List<Tctlg>> GetMobileDBctlg(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBctlg(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBsettings/{app_code}/{code_mobile}")]
        public ActionResult<List<Tsettings>> GetMobileDBsettings(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBsettings(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBquestions/{app_code}/{code_mobile}")]
        public ActionResult<List<Tquestions>> GetMobileDBquestions(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBquestions(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBanswers/{app_code}/{code_mobile}")]
        public ActionResult<List<Tanswers>> GetMobileDBanswers(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBanswers(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBstyles/{app_code}/{code_mobile}")]
        public ActionResult<List<Tstyles>> GetMobileDBstyles(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBstyles(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBpredpr_kart/{app_code}/{code_mobile}")]
        public ActionResult<List<Tpredpr_kart>> GetMobileDBpredpr_kart(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBpredpr_kart(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBpredpr_tov_art/{app_code}/{code_mobile}")]
        public ActionResult<List<Tpredpr_tov_art>> GetMobileDBpredpr_tov_art(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBpredpr_tov_art(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBpredpr_tov_gr/{app_code}/{code_mobile}")]
        public ActionResult<List<Tpredpr_tov_gr>> GetMobileDBpredpr_tov_gr(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBpredpr_tov_gr(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("GetMobileDBtov_gr_tov_art/{app_code}/{code_mobile}")]
        public ActionResult<List<Ttov_gr_tov_art>> GetMobileDBtov_gr_tov_art(string app_code, string code_mobile)
        {
            var response = variaService.GetMobileDBtov_gr_tov_art(app_code, code_mobile);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpGet("RetrieveFile/{filename}/{app_code}")]
        public byte[] RetrieveFile(string filename, string app_code)
        {
            return videoService.RetrieveFile(filename, app_code);
        }

        [HttpGet("GetImageMobile/{app_code}/{img_filename}")]
        public Stream GetImageMobile(string app_code, string img_filename)
        {
            return pictureService.GetImageMobile(app_code, img_filename);
        }

        [HttpGet("GetImageMobileBase64/{app_code}/{img_filename}")]
        public ActionResult<string> GetImageMobileBase64(string app_code, string img_filename)
        {
            var response = pictureService.GetImageMobileBase64(app_code, img_filename);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpGet("GetVideoMobile/{app_code}/{img_filename}")]
        public Stream GetVideoMobile(string app_code, string img_filename)
        {
            return videoService.GetVideoMobile(app_code, img_filename);
        }

        [HttpGet("VideoFileSave/{app_code}/{code_mobile}/{filename}")]
        public ActionResult<TVideoFileSave> VideoFileSave(string app_code, string code_mobile, string filename)
        {
            var response = videoService.VideoFileSave(app_code, code_mobile, filename);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpGet("VideoFileDelete/{app_code}/{code_mobile}/{filename}")]
        public ActionResult<TVideoFileDelete> VideoFileDelete(string app_code, string code_mobile, string filename)
        {
            var response = videoService.VideoFileDelete(app_code, code_mobile, filename);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpGet("GetImageMobile1/{app_code}/{img_filename}")]
        public ActionResult<Tmessage> GetImageMobile1(string app_code, string img_filename)
        {
            var response = pictureService.GetImageMobile1(app_code, img_filename);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpGet("ServerTest")]
        public ActionResult<int> ServerTest()
        {
            return variaService.ServerTest();
        }

        [HttpPost("UpdateBase/{app_code}")]
        public ActionResult<TResult> UpdateBase(string app_code, List<Table> tables)
        {
            var response = baseService.UpdateBase(app_code, tables);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("UpdateTable/{app_code}")]
        public ActionResult<string> UpdateTable(string tableName, string appCode, Table table)
        {
            return baseService.UpdateTable(tableName, appCode, table);
        }

        [HttpPost("DeleteFromBase/{app_code}")]
        public ActionResult<TResult> DeleteFromBase(string app_code, List<TRowToDelete> rowsToDelete)
        {
            var response = baseService.DeleteFromBase(app_code, rowsToDelete);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("UploadPhoto/{appCode}/{fileName}")]
        public ActionResult<TResult> UploadPhoto(string appCode, string fileName, Stream fileContents)
        {
            var response = pictureService.UploadPhoto(appCode, fileName, fileContents);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("SetUpdateAllFlag/{app_code}")]
        public ActionResult<TResult> SetUpdateAllFlag(string app_code)
        {
            var response = variaService.SetUpdateAllFlag(app_code);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("AddInetMobileOrder2/{smsText}/{comment}")]
        public ActionResult<TResult> AddInetMobileOrder2(string smsText, string comment)
        {
            var response = orderService.AddInetMobileOrder2(smsText, comment);
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("AddInetMobileOrder3")]
        public ActionResult<TResult> AddInetMobileOrder3()
        {
            var response = orderService.AddInetMobileOrder3();
            return new JsonResult(response, new JsonSerializerOptions());
        }

        [HttpPost("AddInetMobileOrder")]
        public ActionResult<TResult> AddInetMobileOrder(TOrder order)
        {
            var response = orderService.AddInetMobileOrder(order);
            return new JsonResult(response, new JsonSerializerOptions());
        }
    }
}