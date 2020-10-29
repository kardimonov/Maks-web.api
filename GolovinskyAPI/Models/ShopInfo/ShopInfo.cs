using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Models.ShopInfo
{
    public class ShopInfo
    {
        public string cust_id { get; set; }
        public string name { get; set; }
        public string CatalogBanner { get; set; }
        public bool IsBasketWOPrice { get; set; }
        public bool IsPictureCatalog { get; set; }
        public string MainPicture { get; set; }
        public string MainPictureAccountUser { get; set; }
        public string e_mail { get; set; }
        public string phone { get; set; }
        public string DZ { get; set; }
    }
}
