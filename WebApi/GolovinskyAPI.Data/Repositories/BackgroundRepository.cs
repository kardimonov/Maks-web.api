using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Background;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Repositories
{
    public class BackgroundRepository : IBackgroundRepository
    {        
        public async Task<string> CreateAsync(Background background)
        {
            var res = "";

            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = await db.QueryAsync<string>("sp_SetUpdateBkgImageMobile", new
                {
                    appcode = background.AppCode,
                    filename = background.FileName,
                    img = background.Img,
                    date = background.Date,
                    orient = background.Orient,
                    place = background.Place
                },
                    commandType: CommandType.StoredProcedure);
                res = response.First();
            }

            return res;
        }

        public async Task<string> DeleteAsync(Background background)
        {
            var res = "";
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = await db.QueryAsync<string>("sp_DelBkgImageMobile", new
                {
                    appcode = background.AppCode,
                    filename = background.FileName
                },
                   commandType: CommandType.StoredProcedure);
                res = response.First();
            }

            return res;
        }

        public async Task<List<Background>> GetBackgroundAsync(Background background)
        {
            List<Background> res = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = await db.QueryAsync<Background>("sp_GetBkgImageMobile", new
                {
                    appcode = background.AppCode,
                    date = background.Date,
                    mark = background.Mark,
                    orient = background.Orient,
                    place = background.Place
                },
                    commandType: CommandType.StoredProcedure);
                res = response.ToList();
            }

            return res;
        }

        public async Task<string> UpdateAsync(Background background)
        {
            var res = "";
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var response = await db.QueryAsync<string>("sp_SetUpdateBkgImageMobile", new
                {
                    appcode = background.AppCode,
                    filename = background.FileName,
                    img = background.Img,
                    date = background.Date,
                    orient = background.Orient,
                    place = background.Place
                },
                    commandType: CommandType.StoredProcedure);
                res = response.First();
            }

            return res;
        }
    }
}