using System.ComponentModel.DataAnnotations;

namespace GolovinskyAPI.Data.Models
{
    public class PasswordRecoveryInput
    {
        [Required]
        [EmailAddress]
        public string Phone { get; set; }
        [Required]
        public string Cust_ID_Main { get; set; }
    }
}