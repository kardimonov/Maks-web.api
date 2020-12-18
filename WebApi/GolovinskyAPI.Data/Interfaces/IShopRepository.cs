using GolovinskyAPI.Data.Models.ShopInfo;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IShopRepository : IBaseRepository
    {
        ShopInfo GetSubDomain(string url);
        Task<ShopDetailsGet> GetShopDetailsAsync(int id);
        Task<int> UpdateShopDetailsAsync(ShopDetailsPut model);
    }
}