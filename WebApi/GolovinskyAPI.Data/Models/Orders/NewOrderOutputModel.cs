namespace GolovinskyAPI.Data.Models.Orders
{
    public class NewOrderOutputModel
    {
        //public string Ord_No { get; set; }
            
        public int? Ord_ID { get; set; }
        public bool Status { get; set; } = true;
    }
}