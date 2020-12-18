using Microsoft.Data.SqlClient;
using MobileApi.Data.Interfaces;
using MobileApi.Data.Models;
using System.Data;

namespace MobileApi.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void AddInetMobileOrder(TOrder order)
        {
            var orderContent = order.orderContent;
            var orderComment = order.orderComment;
            var orderPhone = order.orderPhone;

            var strSQL = $"sp_AddInetMobileOrder @String='{orderContent}', @Note='{orderComment}', @Phone='{orderPhone}'";
            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);
        }

        public void AddInetMobileOrder2(string smsText, string comment)
        {
            var strSQL = $"sp_AddInetMobileOrder @String='{smsText}', @Note='{comment}'";
            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);
        }        
    }
}