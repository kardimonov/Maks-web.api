using System.IO;
using System;
using System.Linq;
using GolovinskyAPI.Logic.Interfaces;

namespace GolovinskyAPI.Logic.Services
{
    public class CustomizeService : ICustomizeService
    {
        public string GetMainImage()
        {
            var prefix = "wwwroot/Images\\";
            var date = DateTime.Now.ToString("dd.MM.yyyy");
            var searchresult = prefix + date;
            var directoryFiles = Directory.GetFiles("wwwroot/Images");
            var image = Array.Find(directoryFiles, s => s.StartsWith(searchresult));

            if (directoryFiles.Contains(image))
            {
                var result = image.Substring(image.LastIndexOf('\\') + 1); ;
                return result;
            }
            else
            {
                var random = new Random();
                var index = random.Next(directoryFiles.Length - 1);
                image = directoryFiles[index];
                var result = image.Substring(image.LastIndexOf('\\') + 1);

                return result;
            }            
        }

        public string GetMainImageUserAccount()
        {
            var prefix = "wwwroot/AccountImages\\";
            var date = DateTime.Now.ToString("dd.MM.yyyy");
            var searchresult = prefix + date;
            var directoryFiles = Directory.GetFiles("wwwroot/AccountImages");
            var image = Array.Find(directoryFiles, s => s.StartsWith(searchresult));
            if(directoryFiles.Contains(image))
            {
                var result = image.Substring(image.LastIndexOf('\\') + 1); 
                return result;
            }
            else
            {
                var random = new Random();
                var index = random.Next(directoryFiles.Length - 1);
                image = directoryFiles[index];
                var result = image.Substring(image.LastIndexOf('\\') + 1);

                return result;
            }
        } 
    }
}