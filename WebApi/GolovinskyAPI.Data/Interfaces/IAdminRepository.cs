using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.CustomerInfo;
using System.Collections.Generic;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IAdminRepository : IBaseRepository
    {
        CustomerInfoPromoOutputModel CustomerInfoPromo(int? id);
        LoginAdminOutputModel CheckWebPasswordAdmin(LoginModel input);
        bool UploadDatabaseFromTxt(UploadDbFromTxt upload);
        SearchPictureInfoOutputModel SearchPictureInfo(SearchPictureInfoInputModel input);
        List<SearchPictureOutputModel> SearchProduct(SearchPictureInputModel input);
        List<SearchPictureOutputModel> SearchAllAdminPictures(AdminPictureInfo input);
        bool SetAdvertStatus(ChangeStatusViewModel model);
    }    
}