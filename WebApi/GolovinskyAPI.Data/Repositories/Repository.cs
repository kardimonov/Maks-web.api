using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.ShopInfo;
using GolovinskyAPI.Data.Models.Orders;
using GolovinskyAPI.Data.Models.CustomerInfo;
using GolovinskyAPI.Data.Models.Notification;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GolovinskyAPI.Data.Models.Password;

namespace GolovinskyAPI.Data.Repositories
{
    public class Repository : IRepository
    {        
        public ShopInfo GetSubDomain(string url)
        {
            ShopInfo result;                        
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                result = db.Query<ShopInfo>("sp_GetShopInfo", new { URL = url },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public string[] RecoveryPassword(PasswordRecoveryInput input)
        {
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var answer = db.Query<PasswordRecoveryResponse>("sp_RecoveryPassword", new
                { 
                    Phone = input.Phone, 
                    Cust_ID_Main = input.Cust_ID_Main
                },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                var res = new string[] { answer.Response, answer.Password, answer.SiteTxt };
                return res;
            }
        }

        public CustomerInfoPromoOutputModel CustomerInfoPromo(int? cust_id)
        {
            CustomerInfoPromoOutputModel result;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                result = db.Query<CustomerInfoPromoOutputModel>("sp_GetCustomerInfoPromo", 
                    new { Cust_ID = cust_id }, 
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public bool Pay(NewOrderItemInputModel input)
        {
            string res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<string>("sp_AddNewOrdItem", new
                {
                    OrdTtl_Id = input.OrdTtl_Id,
                    OI_No = input.OI_No,
                    Ctlg_No = input.Ctlg_No,
                    Qty = input.Qty,
                    Ctlg_Name = input.Ctlg_Name,
                    Sup_ID = input.Sup_ID,
                    Descr = input.Descr
                },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return res == "1";
        }

        public async Task<string> RequestGoodsMark(NotificationViewModel model)
        {
            string res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = await db.QueryFirstOrDefaultAsync<string>("sp_RequestGoodsMark", new
                {
                    AppCode = model.AppCode,
                    CID = model.CID,
                    ID = model.ID,
                    Mark = model.Mark
                },
                    commandType: CommandType.StoredProcedure);
            }
            return res;
        }
    }
}