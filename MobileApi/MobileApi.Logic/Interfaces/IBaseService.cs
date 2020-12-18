using MobileApi.Data.Models;
using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Models;
using System.Collections.Generic;

namespace MobileApi.Logic.Interfaces
{
    public interface IBaseService : IService
    {
        List<TGetNewTablesMobileDB> GetNewTablesMobileDB(string app_code, string code_mobile);
        List<TGetNewTablesMobileDB> GetAllTablesMobileDB(string app_code, string code_mobile);
        TResult UpdateBase(string app_code, List<Table> tables);
        string UpdateTable(string tableName, string appCode, Table table);
        TResult DeleteFromBase(string app_code, List<TRowToDelete> rowsToDelete);
    }
}