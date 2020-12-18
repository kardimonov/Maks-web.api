namespace GolovinskyAPI.Data.Models.ShopInfo
{
    public class ShopDetailsPut
    {        
        public string FName { get; set; }        // Company name        
        public string Repres { get; set; }       // Contact person        
        public string Phone { get; set; }        // Phone number        
        public string EMail { get; set; }          
        public string MobileOpt { get; set; }    // Optional mobile phone        
        public string GeoAddress { get; set; }   // Address        
        public string Firma { get; set; }        // Short description of company        
        public string ReturnURL { get; set; }    // Web-site        
        public string EMailBlankRequest { get; set; } // E-mail for contact with developer        
        public string WordEnter { get; set; }    // Sphere of business
        public int Cust_ID_Main { get; set; }    // Shop id
    }
}