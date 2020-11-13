namespace GolovinskyAPI.Logic.Models
{
    public class AuthServiceModel
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifeTime{ get; set; }
        public string Key { get; set; }
    }
}