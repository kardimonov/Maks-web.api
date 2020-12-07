using GolovinskyAPI.Data.Models.Background;
using Microsoft.AspNetCore.Http;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IUploadPicture
    {
        byte[] UploadBase64Image(string uploadImage);

        string GetBase64Image(byte[] byteImage);

        //string GetImagePath(string appCode, string fileName);

        //bool RemoveBackground(Background background);

        byte[] GetImageBytesArray(IFormFile uploadImage);
    }
}