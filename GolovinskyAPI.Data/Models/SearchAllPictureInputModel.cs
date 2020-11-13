using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GolovinskyAPI.Data.Models
{
    public class SearchAllPictureInputModel
    {
        [BindRequired]
        public string SearchDescr { get; set; } = null;
        [BindRequired]
        public int Cust_ID { get; set; }
        [BindRequired]
        public string Suplier { get; set; } = "";
        [BindRequired]
        public string ID { get; set; }
        [BindRequired]
        public int Option { get; set; } = 0;
        [BindRequired]
        public int? CID { get; set; } = null;
    }
}