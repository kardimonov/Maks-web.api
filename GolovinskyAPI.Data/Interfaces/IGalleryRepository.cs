using GolovinskyAPI.Data.Models;
using System.Collections.Generic;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IGalleryRepository : IBaseRepository
    {
        List<SearchPictureOutputModel> SearchPicture(SearchPictureInputModel input);
        List<SearchPictureOutputModel> SearchAllPictures(SearchAllPictureInputModel input);
    }
}