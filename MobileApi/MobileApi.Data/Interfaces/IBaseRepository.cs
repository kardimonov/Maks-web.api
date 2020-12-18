using MobileApi.Data.Models;
using MobileApi.Data.Models.InnerClasses;
using System.Collections.Generic;

namespace MobileApi.Data.Interfaces
{
    public interface IBaseRepository : IRepository
    {
        List<TGetNewTablesMobileDB> GetNewTablesMobileDB(string app_code, string code_mobile);
        List<TGetNewTablesMobileDB> GetAllTablesMobileDB(string app_code, string code_mobile);
        string UpdateRow(string tableName, string appCode, object row);
        string DeleteRow(string appCode, TRowToDelete rtd);
    }
}