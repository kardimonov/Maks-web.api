namespace GolovinskyAPI.Data.Models.Orders
{
    public class NewOrderInputModel
    {
        //id пользователя
         public int Cust_ID { get; set; }
        //код валюты
        public int Cur_Code { get; set; } = 840;
    }
}