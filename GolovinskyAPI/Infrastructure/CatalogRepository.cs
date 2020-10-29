using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using GolovinskyAPI.Infrastructure.Interfaces;
using GolovinskyAPI.Models.Catalog;
using System.Linq;

namespace GolovinskyAPI.Infrastructure
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly string connectionString;

        public CatalogRepository(string connection)
        {
            connectionString = connection;
        }

        public CatalogOutput Create(Catalog catalog)
        {
            CatalogOutput res;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var response = db.Query<CatalogOutput>("sp_CatalogCreateSection", new
                {
                    ID = catalog.Id,
                    Name = catalog.Name,
                    Img_Name = catalog.ImgName,
                    Cust_ID_Main = catalog.CustIdMain
                },
                    commandType: CommandType.StoredProcedure).First();
                res = response;
            }

            return res;
        }

        public CatalogOutput Update(Catalog catalog)
        {
            CatalogOutput res;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var response = db.Query<CatalogOutput>("sp_CatalogEditSection", new
                {
                    ID = catalog.Id,
                    New_Name = catalog.Name,
                    Img_Name = catalog.ImgName,
                    Cust_ID_Main = catalog.CustIdMain
                },
                    commandType: CommandType.StoredProcedure).First();
                res = response;
            }

            return res;
        }

        public string Delete(Catalog catalog)
        {
            string res = "";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var response = db.Query<string>("sp_CatalogDeleteSection", new
                {
                    ID = catalog.Id,
                    Cust_ID_Main = catalog.CustIdMain
                },
                   commandType: CommandType.StoredProcedure).First();
                res = response;
            }

            return res;
        }
    }
}
