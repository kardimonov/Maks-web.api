using MobileApi.Data.Interfaces;
using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Interfaces;
using MobileApi.Logic.Models;
using System;
using System.Drawing;
using System.IO;

namespace MobileApi.Logic.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository repo;

        public PictureService(IPictureRepository repository)
        {
            repo = repository;
        }

        public Stream GetImageMobile(string app_code, string img_filename)
        {
            var data = repo.GetImageMobile(app_code, img_filename);
            
            return new MemoryStream(data)
            {
                Position = 0,
            };
        }

        public string GetImageMobileBase64(string app_code, string img_filename)
        {
            var data = repo.GetImageMobileBase64(app_code, img_filename);            
            var base64String = Convert.ToBase64String(data);
            return base64String;
        }

        public Tmessage GetImageMobile1(string app_code, string img_filename)
        {
            var message = repo.GetImageMobile1(app_code, img_filename);            
            var result = new Tmessage() { message = message };
            return result;
        }

        public TResult UploadPhoto(string appCode, string fileName, Stream fileContents)
        {
            TResult result;
            try
            {
                var image = new Bitmap(fileContents);
                byte[] img;
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    img = ms.ToArray();
                }

                repo.UploadPhoto(appCode, fileName, img);
                result = new TResult() { type = "success", value = "" };
            }
            catch (ArgumentException)
            {
                result = new TResult() { type = "error", value = "Failed to parse multipart data!" };
            }
            return result;
        }
    }
}