using AutoMapper;
using GolovinskyAPI.Data.Models.Background;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models.Background;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с картинками
    /// </summary>
    /// <returns></returns>
    //[Produces("application/json")]
    [Route("api/Background")]
    [ApiController]
    public class BackgroundController : Controller
    {
        private readonly IBackgroundService _service;
        private readonly IUploadPicture _handler;
        private readonly IMapper _mapper;

        public BackgroundController(
            IBackgroundService service,
            IUploadPicture uploadHandler,
            IMapper autoMapper)
        {
            _service = service;
            _handler = uploadHandler;
            _mapper = autoMapper;
        }

        /// <summary>
        /// Получение фона в формате base64
        /// </summary>
        [HttpGet]
        [Route("base64")]
        public async Task<IActionResult> Get(string appCode, char mark, char orientation, char place)
        {
            var tuple = (place, appCode, mark, orientation);
            var background = _mapper.Map<Background>(tuple);
            var result = await _service.GetBackgroundAsync(background);

            //ViewBag.Backgrounds = result;

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                var response = result.Select(background =>
                    new
                    {
                        Image = _handler.GetBase64Image(background.Img),
                        FileName = background.FileName,
                        Orientation = background.Orient,
                        Place = background.Place
                    }).ToList();
                return Ok(response);
            }
        }

        /// <summary>
        /// Добавление фона в формате base64
        /// </summary>
        [HttpPost]
        [Route("base64")]
        public async Task<IActionResult> Post([FromForm] BackgroundPostBase64 model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var bytesImage = _handler.UploadBase64Image(model.Image);
            var background = _mapper.Map<Background>(model, opt => opt.Items["Img"] = bytesImage);
            var response = await _service.CreateAsync(background);

            return Ok(response);
        }

        /// <summary>
        /// Обновление фона в формате base64
        /// </summary>
        [HttpPut]
        [Route("base64")]
        public async Task<IActionResult> Put([FromForm] BackgroundPutBase64 model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var bytesImage = _handler.UploadBase64Image(model.Image);
            var background = _mapper.Map<Background>(model, opt => opt.Items["Img"] = bytesImage);
            var response = await _service.UpdateAsync(background);

            return Ok(response);
        }

        /// <summary>
        /// Получение фона в file формате
        /// </summary>
        [HttpGet]
        [Route("file")]
        public async Task<IActionResult> Get(char place, string appCode, char mark, char orientation)
        {
            var tuple = (place, appCode, mark, orientation);
            var background = _mapper.Map<Background>(tuple);
            var result = await _service.GetBackgroundAsync(background);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                var response = result.Select(background =>
                    new
                    {
                        FileName = background.FileName,
                        Orientation = background.Orient,
                        Place = background.Place
                    }).ToList();
                return Ok(response);
            }
        }

        /// <summary>
        /// Добавление фона в file формате
        /// </summary>
        [HttpPost]
        [Route("file")]
        public async Task<IActionResult> Post([FromForm] BackgroundPostFile model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var bytesImage = _handler.GetImageBytesArray(model.Image);
            var background = _mapper.Map<Background>(model, opt => opt.Items["Img"] = bytesImage);
            var response = await _service.CreateAsync(background);

            return Ok(response);
        }

        /// <summary>
        /// Обновление фона в file формате
        /// </summary>
        [HttpPut]
        [Route("file")]
        public async Task<IActionResult> Put([FromForm] BackgroundPutFile model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var bytesImage = _handler.GetImageBytesArray(model.Image);
            var background = _mapper.Map<Background>(model, opt => opt.Items["Img"] = bytesImage);
            var response = await _service.UpdateAsync(background);

            return Ok(response);
        }

        /// <summary>
        /// Удаление фона формата base64
        /// </summary>
        [HttpDelete]
        [Route("base64")]
        public async Task<IActionResult> Delete([FromBody] BackgroundBase64Delete model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Не верные параметры в запросе");
            }

            var background = _mapper.Map<Background>(model);
            var response = await _service.DeleteAsync(background);

            if (response.Response == "1")
            {
                return Ok(response);
            }                
            else
            {
                return BadRequest(response);
            }                
        }
    }
}