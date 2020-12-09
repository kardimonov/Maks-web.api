using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        public List<SearchPictureOutputModel> SearchPicture(SearchPictureInputModel input)
        {
            List<SearchPictureOutputModel> res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                // new { SearchDescr = input.SearchDescr, Cust_ID = input.Cust_ID, ID = input.ID }
                res = db.Query<SearchPictureOutputModel>("sp_SearchPicture", input,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }

        public List<SearchPictureOutputModel> SearchAllPictures(SearchAllPictureInputModel input)
        {
            List<SearchPictureOutputModel> res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<SearchPictureOutputModel>("sp_SearchPicture", input,
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return res;
        }
    }
}