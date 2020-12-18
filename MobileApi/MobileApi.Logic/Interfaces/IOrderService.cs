using MobileApi.Data.Models;
using MobileApi.Logic.Models;

namespace MobileApi.Logic.Interfaces
{
    public interface IOrderService : IService
    {
        TResult AddInetMobileOrder(TOrder order);
        TResult AddInetMobileOrder2(string smsText, string comment);
        TResult AddInetMobileOrder3();
    }
}