namespace GolovinskyAPI.Data.Models.Authorization
{
    public class RegisterOutputModel
    {
        public int Cust_ID { get; set; }
        public int Comp_ID { get; set; }
        public string AuthCode { get; set; }
        public string AuthPass { get; set; }
        public bool Result { get; set; } = true;
        public string Txt { get; set; }
    }
}