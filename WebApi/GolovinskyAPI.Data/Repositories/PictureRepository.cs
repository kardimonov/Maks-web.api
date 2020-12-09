using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Images;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IDbConnection dbConnection;

        public PictureRepository()
        {
            dbConnection = new SqlConnection(Global.Connection);
        }

        // ПРОЦЕДУРА НЕ РАБОТАЕТ
        public byte[] GetImageMobile(string id, string name)
        {
            var res = new byte[0];
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var resObj = db.Query<byte[]>("sp_GetImageMobile", 
                    new { AppCode = id, img_filename = name },
                    commandType: CommandType.StoredProcedure).First();
                res = resObj;
            }
            return res;
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
            return res;
        }

        // Показ дополнительных картинок
        public List<Picture> GetAllAdditionalPictures(SearchPictureInfoInputModel input)
        {
            List<Picture> list = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                //search additional images
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

        // добавление картинки в базу
        public string UploadPicture(NewUploadImageInputByte result)
        {
            string resObj;
            using (dbConnection)
            {
                resObj = dbConnection.Query<string>("sp_UploadMobileDBPictAll", result,
                    commandType: CommandType.StoredProcedure).First();
            }
            return resObj ;
        }

        // удаление главной картинки к товару или объявлению
        public bool DeleteMainPicture(SearchPictureInfoInputModel input)
        {
            string resObj;
            using (dbConnection)
            {
                resObj = dbConnection.Query<string>("sp_SearchDeleteAvito", input,
                    commandType: CommandType.StoredProcedure).First();
            }
            return resObj == "1";
        }
    }
}