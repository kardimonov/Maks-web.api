﻿namespace GolovinskyAPI.Data.Models.Admin
{
    public class LoginAdminOutputModel
    {
        public string accessToken { get; set; }
        public int Cust_ID { get; set; }
        public int Cust_ID_Main { get; set; }
        public string Role { get; set; }
        public string Txt { get; set; }
    }
}