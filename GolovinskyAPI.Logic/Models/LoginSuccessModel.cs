namespace GolovinskyAPI.Logic.Models
{
    public class LoginSuccessModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string WhatsApp { get; set; }
        public string Skype { get; set; }
        public string Email { get; set; }

        // фон сайта для пользователя
        public string MainImage { get; set; }
    }
}