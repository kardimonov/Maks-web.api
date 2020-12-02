using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GolovinskyAPI.Data.Models.Categories
{
    public class SearchAvitoPictureInput
    {
        [BindRequired]
        public string Catalog { get; set; }
        [BindRequired]
        public int Table { get; set; } = 1;
        [BindRequired]
        public string Id { get; set; }
        [BindRequired]
        public string Cust_ID_Main { get; set; }
    }
}