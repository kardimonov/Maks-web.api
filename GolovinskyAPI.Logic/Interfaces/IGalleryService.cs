using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Models.Gallery;
using System.Collections.Generic;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IGalleryService : IBaseService
    {
        List<SearchPictureOutputModel> SearchPicture(SearchPictureInputModel model);
        GalleryViewModel SearchPicture(SearchPictureInputModel model, int itemsPerPage, int currentPage, int sort);
        GalleryViewModel SearchAllPictures(SearchAllPictureInputModel model, int itemsPerPage, int currentPage);
    }
}