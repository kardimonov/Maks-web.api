using MobileApi.Data.Models.InnerClasses;
using System.IO;

namespace MobileApi.Logic.Interfaces
{
    public interface IVideoService : IService
    {
        Stream GetVideoMobile(string app_code, string img_filename);
        TVideoFileSave VideoFileSave(string app_code, string code_mobile, string filename);
        TVideoFileDelete VideoFileDelete(string app_code, string code_mobile, string filename);
        byte[] RetrieveFile(string filename, string app_code);
    }
}