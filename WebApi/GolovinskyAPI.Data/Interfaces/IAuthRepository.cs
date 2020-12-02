using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Data.Models.CustomerInfo;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IAuthRepository : IBaseRepository
    {
        int CheckWebPassword(LoginModel input);
        CustomerInfoOutput GetCustomerFIO(int CustID);
        RegisterOutputModel AddWebCustomerCompany(RegisterInputModel input);
        Task<UpdateUserOutputModel> UpdateUser(UpdateUserModel model);
    }
}