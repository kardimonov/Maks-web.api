using Dapper;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Admin;
using GolovinskyAPI.Data.Models.CustomerInfo;
using GolovinskyAPI.Data.Models.Images;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbConnection dbConnection;

        public AdminRepository()
        {
            dbConnection = new SqlConnection(Global.Connection);
        }

        public CustomerInfoPromoOutputModel CustomerInfoPromo(int? cust_id)
        {
            CustomerInfoPromoOutputModel result;
            using (var db = new SqlConnection(Global.Connection))
            {
                result = db.Query<CustomerInfoPromoOutputModel>("sp_GetCustomerInfoPromo",
                    new { Cust_ID = cust_id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public LoginAdminOutputModel CheckWebPasswordAdmin(LoginModel input)
        {
            LoginAdminOutputModel res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = db.Query<LoginAdminOutputModel>("sp_CheckWebPasswordAdmin", new 
                { 
                    UserName = input.UserName, 
                    Password = input.Password
                },
                    commandType: CommandType.StoredProcedure).First();
                res = response;
            }
            return res;
        }

        public bool UploadDatabaseFromTxt(UploadDbFromTxt input)
        {
            try
            {
                string result;
                using (dbConnection)
                {
                    result = dbConnection.Query<string>("sp_UpdateMobileDBfromTxtFiles", input,
                        commandType: CommandType.StoredProcedure).First();
                }
                return result == "1";
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SearchPictureInfoOutputModel SearchPictureInfo(SearchPictureInfoInputModel input)
        {
            SearchPictureInfoOutputModel res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<SearchPictureInfoOutputModel>("sp_SearchPictureInfo",
                   new {
                       Prc_ID = input.Prc_ID,
                       Cust_ID = input.Cust_ID,
                       AppCode = input.AppCode
                   },
                   commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            if (res != null)
            {
                res.AdditionalImages = this.GetAllAdditionalPictures(input);
                res.Prc_ID = input.Prc_ID;
                return res;
            }
            return null;
        }

        private List<Picture> GetAllAdditionalPictures(SearchPictureInfoInputModel input)
        {
            List<Picture> list = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                list = db.Query<Picture>("sp_SearchGetAvitoAddImage",
                    new {
                        Prc_ID = input.Prc_ID,
                        Cust_ID = input.Cust_ID,
                        AppCode = input.AppCode
                    },
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return list;
        }

        public List<SearchPictureOutputModel> SearchProduct(SearchPictureInputModel input)
        {
            List<SearchPictureOutputModel> response = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                response = db.Query<SearchPictureOutputModel>("sp_SearchPicture",
                    new {
                        SearchDescr = input.SearchDescr,
                        Cust_ID = input.Cust_ID
                    },
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return response;
        }

        public List<SearchPictureOutputModel> SearchAllAdminPictures(AdminPictureInfo input)
        {
            List<SearchPictureOutputModel> res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<SearchPictureOutputModel>("sp_SearchPicture", input,
                    commandType: CommandType.StoredProcedure).ToList();
            }            
            return res;
        }

        public bool SetAdvertStatus(ChangeStatusViewModel model)
        {
            var res = false;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<bool>("sp_ShowAdvert", new
                {
                    Prc_Id = model.Id,
                    Cust_ID_Main = model.CustIdMain,
                    IsHid = model.IsHid,
                },
                commandType: CommandType.StoredProcedure).First();
            }
            return res;
        }
    }
}