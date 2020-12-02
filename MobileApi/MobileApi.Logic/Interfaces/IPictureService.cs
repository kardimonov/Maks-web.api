using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Models;
using System.IO;

namespace MobileApi.Logic.Interfaces
{
    public interface IPictureService : IService
    {
        Stream GetImageMobile(string app_code, string img_filename);
        string GetImageMobileBase64(string app_code, string img_filename);
        Tmessage GetImageMobile1(string app_code, string img_filename);
        TResult UploadPhoto(string appCode, string fileName, Stream fileContents);
    }
}