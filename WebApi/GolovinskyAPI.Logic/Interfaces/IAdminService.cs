using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Text;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IAdminService : IBaseService
    {
        GalleryViewModel SearchAllAdminPictures(AdminPictureInfo dto, int itemsPerPage, int currentPage);
    }
}