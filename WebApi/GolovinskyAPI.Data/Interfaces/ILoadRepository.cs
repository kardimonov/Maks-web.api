using System.Collections.Generic;
using GolovinskyAPI.Data.Models.Categories;
using GolovinskyAPI.Data.Models.Mobile;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface ILoadRepository : IBaseRepository
    {
        int GetCustId(int subdomain);
        List<SearchAvitoPictureOutput> SearchAvitoPicture(SearchAvitoPictureInput input);
        List<SearchAvitoPictureOutput> GetCategoryItems(CategoriesInput input);
        List<OutMobileDbModel> GetMobileDB(GetMobileDbModel input);
        bool AddInetMobileOrder(AddInetMobileOrdeModel input);
    }
}