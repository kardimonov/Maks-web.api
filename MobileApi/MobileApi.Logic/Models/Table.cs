using MobileApi.Data.Models.InnerClasses;
using System.Collections.Generic;

namespace MobileApi.Logic.Models
{
    public class Table
    {
        public string tableName { get; set; }
        public List<Tpredpr> predpr { get; set; }
        public List<Ttov_gr> tov_gr { get; set; }
        public List<Ttov_art> tov_art { get; set; }
        public List<Ttov_img> tov_img { get; set; }
        public List<Ttov_img_base64> tov_img_base64 { get; set; }
        public List<Tpredpr_kart> predpr_kart { get; set; }
        public List<Tpredpr_tov_gr> predpr_tov_gr { get; set; }
        public List<Tpredpr_tov_art> predpr_tov_art { get; set; }
        public List<Ttov_gr_tov_art> tov_gr_tov_art { get; set; }
        public List<Tsettings> settings { get; set; }
        public List<Ttov_contacts> tov_contacts { get; set; }
        public List<Tstyles> styles { get; set; }
        public List<Tanswers> answers { get; set; }
        public List<Tquestions> questions { get; set; }
    }
}