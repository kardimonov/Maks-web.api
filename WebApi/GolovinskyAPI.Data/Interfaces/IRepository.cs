using System.Threading.Tasks;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.CustomerInfo;
using GolovinskyAPI.Data.Models.ShopInfo;
using GolovinskyAPI.Data.Models.Notification;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IRepository : IBaseRepository
    {        
        string[] RecoveryPassword(PasswordRecoveryInput input);
        CustomerInfoPromoOutputModel CustomerInfoPromo(int? id);        
        ShopInfo GetSubDomain(string url);        
        Task<string> RequestGoodsMark(NotificationViewModel model);
    }
}