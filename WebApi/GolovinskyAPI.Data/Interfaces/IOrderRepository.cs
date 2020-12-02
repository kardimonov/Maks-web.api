using GolovinskyAPI.Data.Models.Orders;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IOrderRepository : IBaseRepository
    {
        NewOrderOutputModel AddNewOrder(NewOrderInputModel input);
        bool AddItemToCart(NewOrderItemInputModel input);
        bool ChangeQty(NewOrderItemInputModel input);
        bool SaveOrder(NewOrderShippingInputModel input);
    }
}