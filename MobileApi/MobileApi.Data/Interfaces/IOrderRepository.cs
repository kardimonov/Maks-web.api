using MobileApi.Data.Models;

namespace MobileApi.Data.Interfaces
{
    public interface IOrderRepository : IRepository
    {
        void AddInetMobileOrder(TOrder order);
        void AddInetMobileOrder2(string smsText, string comment);
    }
}