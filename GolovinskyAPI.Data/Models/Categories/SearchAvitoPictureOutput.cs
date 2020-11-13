using System.Collections.Generic;

namespace GolovinskyAPI.Data.Models.Categories
{
    public class SearchAvitoPictureOutput
    {
        public string id { get; set; }
        //родительская категория
        public string parent_id { get; set; } 
        public string txt { get; set; }
        public string cust_id { get; set; }
        public string picture { get; set; }
        public string isshow { get; set; }
        public int isrequest { get; set; }
        public List<SearchAvitoPictureOutput> ListInnerCat { get; set; } = new List<SearchAvitoPictureOutput>(); 
    }
}