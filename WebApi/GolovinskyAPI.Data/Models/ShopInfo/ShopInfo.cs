namespace GolovinskyAPI.Data.Models.ShopInfo
{
    public class ShopInfo
    {
        public string Cust_id { get; set; }
        public string Name { get; set; }
        public string CatalogBanner { get; set; }
        public bool IsBasketWOPrice { get; set; }
        public bool IsPictureCatalog { get; set; }
        public string MainPicture { get; set; }
        public string MainPictureAccountUser { get; set; }
        public string DZ { get; set; }
        public string E_mail { get; set; }
        public string Phone { get; set; }
        public string ShortDescr { get; set; }
        public string Addr { get; set; }
        public string Welcome { get; set; }
        public string Manual { get; set; }
    }
}