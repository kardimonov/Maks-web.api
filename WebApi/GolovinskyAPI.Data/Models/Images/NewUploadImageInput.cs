using Microsoft.AspNetCore.Http;

namespace GolovinskyAPI.Data.Models.Images
{
    public class NewUploadImageInput
    {
        // id магазина
        public string AppCode { get; set; }
        // наименование картинки
        public string TImageprev { get; set; }
        // сама картинка
        public  IFormFile Img { get; set; }
    }
}