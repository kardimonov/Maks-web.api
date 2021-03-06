namespace GolovinskyAPI.Data.Models.Orders
{
    public class NewOrderShippingInputModel
    {
        // номер заказа – выходной параметр @Ord_ID процедуры [dbo].[sp_AddNewOrder]
        public int Ord_ID { get; set; }
        // адрес доставки
        public string Addr { get; set; }
        // дополнительная информация
        public string Note { get; set; }
        // id пользователя
        public int Cust_ID { get; set; }
    }
}