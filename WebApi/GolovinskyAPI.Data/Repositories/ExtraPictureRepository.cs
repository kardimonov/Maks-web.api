using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Images;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class ExtraPictureRepository : IExtraPictureRepository
    {
        private readonly IDbConnection dbConnection;

        public ExtraPictureRepository()
        {
            dbConnection = new SqlConnection(Global.Connection);
        }

        // добавление дополнительной картинки к товару или объявлению
        public bool InsertAdditionalPictureToProduct(NewAdditionalPictureInputModel input)
        {
            string resObj;
            using (dbConnection)
            {
                resObj = dbConnection.Query<string>("sp_SearchCreateAvitoAddImage", input,
                    commandType: CommandType.StoredProcedure).First();
            }
            return resObj == "1";
        }

        // изменение дополнительной картинки к товару или объявлению
        public bool UpdateAdditionalPictureToProduct(NewAdditionalPictureInputModel input)
        {
            string resObj;
            using (dbConnection)
            {
                resObj = dbConnection.Query<string>("sp_SearchUpdateAvitoAddImage", input,
                    commandType: CommandType.StoredProcedure).First();
            }
            return resObj == "1";
        }

        // удаление дополнительной картинки к товару или объявлению
        public bool DeleteAdditionalPictureToProduct(DeleteAdditionalInputModel input)
        {
            string resObj;
            using (dbConnection)
            {
                resObj = dbConnection.Query<string>("sp_SearchDeleteAvitoAddImage", input,
                    commandType: CommandType.StoredProcedure).First();
            }
            return (resObj == "1");
        }
    }
}