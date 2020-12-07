using GolovinskyAPI.Data.Models.Background;
using GolovinskyAPI.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GolovinskyAPI.Logic.Handlers
{
    public class UploadPictureHandler : IUploadPicture
    {
        private readonly IHostEnvironment _env;

        public UploadPictureHandler(IHostEnvironment env)
        {
            _env = env;
        }

        public string GetBase64Image(byte[] byteImage) //ConvertByteArrayToString
        {
            var image = Convert.ToBase64String(byteImage);
            return image;
        }

        public byte[] UploadBase64Image(string uploadImage)
        {
            var imageBytes = Convert.FromBase64String(uploadImage);
            return imageBytes;
        }

        public byte[] GetImageBytesArray(IFormFile uploadImage)
        {
            var image = Image.FromStream(uploadImage.OpenReadStream(), true, true);
            var size = new Size(image.Width, image.Height);
            var bmp = new Bitmap(image, size);
            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                fileBytes = ms.ToArray();
            }

            return fileBytes;
        }

        //public string GetImagePath(string appCode, string fileName)
        //{
        //    var webRoot = _env.ContentRootPath;
        //    var directory = Path.Combine(webRoot, "wwwroot", "images", "backgrounds", appCode);

        //    if (!Directory.Exists(directory))
        //    {
        //        var dir = Directory.CreateDirectory(directory);
        //    }

        //    var path = Path.Combine(directory, fileName);

        //    return path;
        //}

        //public bool RemoveBackground(Background background)
        //{
        //    var webRoot = _env.ContentRootPath;
        //    var path = Path.Combine(webRoot, "wwwroot", "images", "backgrounds", background.AppCode, background.FileName);

        //    if (!Directory.Exists(path))
        //    {
        //        return false;
        //    }

        //    else
        //    {
        //        Directory.Delete(path);
        //        return true;
        //    }
        //}
    }
}