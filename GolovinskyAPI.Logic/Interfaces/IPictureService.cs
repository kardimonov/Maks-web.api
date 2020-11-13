using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Images;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IPictureService : IBaseService
    {
        bool UploadPicture(NewUploadImageInput input);
        SearchPictureInfoOutputModel SearchPictureInfo(SearchPictureInfoInputModel input);
    }
}