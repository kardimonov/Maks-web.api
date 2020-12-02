using System.ComponentModel.DataAnnotations;

namespace GolovinskyAPI.Data.Models.Authorization
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Cust_ID_Main { get; set; }
        public int Result { get; set; }
    }
}