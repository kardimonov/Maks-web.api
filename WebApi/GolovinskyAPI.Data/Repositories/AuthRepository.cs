using Dapper;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.CustomerInfo;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public int CheckWebPassword(LoginModel input)
        {
            var res = 0;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                DynamicParameters p = new();
                p.Add("@UserName", input.UserName);
                p.Add("@Password", input.Password);
                p.Add("@Cust_ID_Main", input.Cust_ID_Main);
                p.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 50);
                var procRes = db.Execute("sp_CheckWebPassword", p,
                             commandType: CommandType.StoredProcedure);
                res = p.Get<int>("@Result");
            }
            return res;
        }

        public CustomerInfoOutput GetCustomerFIO(int CustID)
        {
            CustomerInfoOutput res = new();
            using (SqlConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<CustomerInfoOutput>("sp_GetCustomerInfo", new { Cust_ID = CustID }, 
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return res;
        }

        public RegisterOutputModel AddWebCustomerCompany(RegisterInputModel input)
        {
            RegisterOutputModel res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                DynamicParameters p = new();
                p.Add("@password", input.password);
                p.Add("@Street", input.Address);
                p.Add("@Phone1", input.Phone1);
                p.Add("@Mobile", input.Mobile);
                p.Add("@whatsapp", input.WhatsApp);
                p.Add("@skype", input.Skype);
                p.Add("@f", input.f);
                p.Add("@e_mail", input.e_mail);
                p.Add("@Cust_ID_Main", input.Cust_ID_Main);
                p.Add("@Cust_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Comp_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@AuthCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@AuthPass", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
                p.Add("@Txt", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                var procRes = db.Execute("sp_AddWebCustomerCompany", p,
                             commandType: CommandType.StoredProcedure);
                res = new RegisterOutputModel
                {
                    Cust_ID = p.Get<int>("@Cust_ID"),
                    Comp_ID = p.Get<int>("@Comp_ID"),
                    AuthCode = p.Get<string>("@AuthCode"),
                    AuthPass = p.Get<string>("@AuthPass"),
                    Txt = p.Get<string>("@Txt")
                };
            }
            return res;
        }

        public async Task<UpdateUserOutputModel> UpdateUser(UpdateUserModel model)
        {
            var res = new UpdateUserOutputModel();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                DynamicParameters p = new();
                p.Add("@password", model.Password);
                p.Add("@Phone1", model.Phone);
                p.Add("@whatsapp", model.WhatsApp);
                p.Add("@skype", model.Skype);
                p.Add("@f", model.f);
                p.Add("@e_mail", model.Email);
                p.Add("@Cust_ID_Main", model.Cust_ID_Main);
                p.Add("@Cust_ID", model.Cust_ID);
                p.Add("@Street", model.Street);
                p.Add("@Comp_ID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@Txt", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                var procRes = await db.ExecuteAsync("sp_UpdateWebCustomerCompany", p,
                             commandType: CommandType.StoredProcedure);

                res = new UpdateUserOutputModel
                {
                    Cust_ID = p.Get<string>("@Cust_ID"),
                    Comp_ID = p.Get<string>("@Comp_ID"),
                    Txt = p.Get<string>("@Txt")
                };
            }
            return res;
        }
    }
}