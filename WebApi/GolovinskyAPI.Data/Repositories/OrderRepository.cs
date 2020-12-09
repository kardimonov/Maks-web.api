using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Orders;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public NewOrderOutputModel AddNewOrder(NewOrderInputModel input)
        {
            NewOrderOutputModel result = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                DynamicParameters p = new();
                p.Add("@Cust_ID", input.Cust_ID);
                p.Add("@Cur_Code", input.Cur_Code);
                p.Add("@Ord_ID", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 10);
                p.Add("@Ord_No", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                var res = db.Execute("sp_AddNewOrder", p, commandType: CommandType.StoredProcedure);

                result = new NewOrderOutputModel
                {
                    Ord_ID = p.Get<int?>("@Ord_ID"),
                };
            }
            return result;
        }

        public bool AddItemToCart(NewOrderItemInputModel input)
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

        /* Для изменения количества по позициям, чтобы обезопасить себя от отключения от канала 
        интернета может быть применена процедура, которая сразу меняет количество в базе, 
        причем, если параметр @NewQty сделать равным 0, то позиция из базы удаляется автоматически. 
        */
        public bool ChangeQty(NewOrderItemInputModel input)
        {
            bool res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<bool>("sp_ChangeQty", new
                {
                    Ord_ID = input.OrdTtl_Id,
                    OI_No = input.OI_No,
                    NewQty = input.Qty,
                },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return res;
        }

        public bool SaveOrder(NewOrderShippingInputModel input)
        {
            string res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<string>("sp_OrderAsSMS", input,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return res.Length != 0;
        }
    }
}