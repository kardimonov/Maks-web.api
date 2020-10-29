using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Models
{
    public class UpdateUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string WhatsApp { get; set; }
        public string Skype { get; set; }
        public string Cust_ID { get; set; }
        public string Cust_ID_Main { get; set; }
        public string f { get; set; }
        public string Street { get; set; }
    }
}
