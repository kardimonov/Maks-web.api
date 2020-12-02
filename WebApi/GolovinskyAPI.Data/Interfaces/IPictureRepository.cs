using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Images;
using System.Collections.Generic;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IPictureRepository : IBaseRepository
    {
        byte[] GetImageMobile(string id, string name);
        SearchPictureInfoOutputModel SearchPictureInfo(SearchPictureInfoInputModel input);
        string UploadPicture(NewUploadImageInputByte result);
        bool DeleteMainPicture(SearchPictureInfoInputModel input);
        List<Picture> GetAllAdditionalPictures(SearchPictureInfoInputModel input);
    }
}