using System;

namespace GolovinskyAPI.Logic.Models.Admin
{
    public class AdminGalleryViewModel
    {
        public int Cust_ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Search { get; set; } = "";
    }
}