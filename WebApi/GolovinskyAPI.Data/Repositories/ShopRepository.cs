using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.ShopInfo;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Repositories
{
    public class ShopRepository : IShopRepository
    {
        public ShopInfo GetSubDomain(string url)
        {
            ShopInfo result;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                result = db.Query<ShopInfo>("sp_GetShopInfo", new 
                { 
                    URL = url 
                },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public async Task<ShopDetailsGet> GetShopDetailsAsync(int id)
        {
            ShopDetailsGet result;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = await db.QueryAsync<ShopDetailsGet>("sp_GetMobileDBfromSite", new
                {
                    Cust_ID = id
                },
                    commandType: CommandType.StoredProcedure);
                result = response.FirstOrDefault();
            }

            return result;
        }

        public async Task<int> UpdateShopDetailsAsync(ShopDetailsPut model)
        {
            var result = 0;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = await db.QueryAsync<int>("sp_UpdateMobileDBfromSite", new
                {
                    model.FName,
                    model.Repres,
                    model.Phone,
                    model.EMail,
                    model.MobileOpt,
                    model.GeoAddress,
                    model.Firma,
                    model.ReturnURL,
                    model.EMailBlankRequest,
                    model.WordEnter,
                    model.Cust_ID_Main
                },
                    commandType: CommandType.StoredProcedure);
                result = response.FirstOrDefault();
            }
            return result;
        }
    }
}