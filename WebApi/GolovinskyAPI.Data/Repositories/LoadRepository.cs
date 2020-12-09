using Dapper;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Categories;
using GolovinskyAPI.Data.Models.Load;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class LoadRepository : ILoadRepository
    {
        public int GetCustId(int subdomain)
        {
            var res = 0;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var resObj = db.Query<GuestCastIDModel>("sp_GetGuestCustID", 
                    new { Cust_ID_Main = subdomain },
                    commandType: CommandType.StoredProcedure).First();
                res = resObj.Cust_ID;
            }
            return res;
        }

        public List<SearchAvitoPictureOutput> SearchAvitoPicture(SearchAvitoPictureInput input)
        {
            List<SearchAvitoPictureOutput> res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<SearchAvitoPictureOutput>("sp_SearchAvitoPicture",
                    new { 
                        Catalog = input.Catalog, 
                        Table = input.Table, Id = input.Id, 
                        Cust_ID_Main = input.Cust_ID_Main 
                    },
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        // get all menu items
        public List<SearchAvitoPictureOutput> GetCategoryItems(CategoriesInput input)
        {
            List<SearchAvitoPictureOutput> categoryList = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                categoryList = db.Query<SearchAvitoPictureOutput>("sp_SearchAvitoPictureAll",
                    input, commandType: CommandType.StoredProcedure).ToList();
            }
            return categoryList;
        }

        public List<OutMobileDbModel> GetMobileDB(GetMobileDbModel input)
        {
            List<OutMobileDbModel> res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<OutMobileDbModel>("sp_GetMobileDB", new
                {
                    AppCode = input.AppCode,
                    tablename = input.TableName,
                    CodeMobile = input.CodeMobile,
                },
                commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public bool AddInetMobileOrder(AddInetMobileOrdeModel input)
        {
            string res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<string>("sp_AddInetMobileOrder", new
                {
                    String = input.String,
                    Note = input.Note,
                    Phone = input.Phone
                },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return res == "1";
        }
    }
}