using GolovinskyAPI.Data.Models.Admin;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Logic.Models.Gallery;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IAdminService : IBaseService
    {
        GalleryViewModel SearchAllAdminPictures(AdminPictureInfo dto, int itemsPerPage, int currentPage);

        LoginAdminOutputModel CheckWebPasswordAdmin(LoginModel loginModel, string userName, string audience);
    }
}