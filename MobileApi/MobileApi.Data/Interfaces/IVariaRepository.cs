using System.Data;
using System.IO;

namespace MobileApi.Data.Interfaces
{
    public interface IVariaRepository : IRepository
    {
        DataTable ServerTest();
        int CheckMobileAppCodeUrl(string app_code, string url);
        int CheckPasswordMobileChief(string app_code, string url, int result);
        void UpdateMarkMobileDB(string app_code, string code_mobile, string tablename);
        string GetMobileShowLogin(string app_code);
        DataTable SetUpdateAllFlag(string app_code);        
        Stream GetImageMobile(string app_code, string img_filename);
        DataTable GetDataTable(string app_code, string tablename, string code_mobile);
    }
}