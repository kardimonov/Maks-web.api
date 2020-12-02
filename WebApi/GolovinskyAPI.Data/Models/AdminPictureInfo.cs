using System;

namespace GolovinskyAPI.Data.Models
{
    public class AdminPictureInfo
    {
        public int Cust_ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string SearchDescr { get; set; }
    }
}