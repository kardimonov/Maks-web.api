using System.Collections.Generic;
using System.Linq;
using System.Data;
using GolovinskyAPI.Data.Models.Categories;

namespace GolovinskyAPI.Logic.Infrastructure
{
    public class CategoryRecursion
    {
        public List<SearchAvitoPictureOutput> GenerateCategories(List<SearchAvitoPictureOutput> listOnputModel)
        {
            var parentsCategories = (
                from a in listOnputModel 
                where a.parent_id == "0" select a).ToList();
            foreach (var parentCat in parentsCategories)
            {
                if (parentsCategories.Count > 0)
                {
                    AddChildItem(parentCat, listOnputModel);
                }
                else
                {
                    continue;
                }   
            } 
            return parentsCategories;
        }

        private void AddChildItem(SearchAvitoPictureOutput parentItem, List<SearchAvitoPictureOutput> listAllCategories)
        {
            var childItems = (
                    from a in listAllCategories 
                    where a.parent_id == parentItem.id select a).ToList();
            foreach (var childItem in childItems)
            {
                if (childItems.Count > 0)
                {
                    parentItem.ListInnerCat.Add(childItem);
                    AddChildItem(childItem, listAllCategories);
                } 
                else 
                {
                    continue;
                }
            }
        }
    }
}