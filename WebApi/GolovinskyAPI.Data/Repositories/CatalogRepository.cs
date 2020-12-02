using Dapper;
using GolovinskyAPI.Data.Models.Catalog;
using GolovinskyAPI.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        public CatalogOutput Create(Catalog catalog)
        {
            CatalogOutput res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<CatalogOutput>("sp_CatalogCreateSection", new
                {
                    ID = catalog.Id,
                    Name = catalog.Name,
                    Img_Name = catalog.ImgName,
                    Cust_ID_Main = catalog.CustIdMain
                },
                commandType: CommandType.StoredProcedure).First();            }
            return res;
        }

        public CatalogOutput Update(Catalog catalog)
        {
            CatalogOutput res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<CatalogOutput>("sp_CatalogEditSection", new
                {
                    ID = catalog.Id,
                    New_Name = catalog.Name,
                    Img_Name = catalog.ImgName,
                    Cust_ID_Main = catalog.CustIdMain
                },
                commandType: CommandType.StoredProcedure).First();
            }
            return res;
        }

        public string Delete(Catalog catalog)
        {
            var res = "";
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<string>("sp_CatalogDeleteSection", new
                {
                    ID = catalog.Id,
                    Cust_ID_Main = catalog.CustIdMain
                },
                   commandType: CommandType.StoredProcedure).First();
            }
            return res;
        }
    }
}