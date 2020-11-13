using GolovinskyAPI.Data.Models.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IExtraPictureRepository : IBaseRepository
    {
        bool InsertAdditionalPictureToProduct(NewAdditionalPictureInputModel input);
        bool UpdateAdditionalPictureToProduct(NewAdditionalPictureInputModel input);
        bool DeleteAdditionalPictureToProduct(DeleteAdditionalInputModel input);
    }
}