using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Models;
using System.Collections.Generic;

namespace MobileApi.Logic.Interfaces
{
    public interface IVariaService : IService
    {
        int ServerTest();
        TCheckMobileAppCodeUrl CheckMobileAppCodeUrl(string app_code, string url);
        TUpdateMarkMobileDB UpdateMarkMobileDB(string app_code, string code_mobile, string tablename);
        TGetMobileShowLogin GetMobileShowLogin(string app_code);
        TResult SetUpdateAllFlag(string app_code);
        List<Tpredpr> GetMobileDBpredpr(string app_code, string code_mobile);
        List<Ttov_gr> GetMobileDBtov_gr(string app_code, string code_mobile);
        List<Ttov_art> GetMobileDBtov_art(string app_code, string code_mobile);
        List<Ttov_img> GetMobileDBtov_img(string app_code, string code_mobile);
        List<Ttov_img> GetMobileDBtov_img_async(string app_code, string code_mobile);
        List<Ttov_img> GetMobileDBtov_img_sync(string app_code, string code_mobile);
        List<Ttov_img_base64> GetMobileDBtov_img_base64_async(string app_code, string code_mobile);
        List<Ttov_contacts> GetMobileDBtov_contacts(string app_code, string code_mobile);
        List<Tctlg> GetMobileDBctlg(string app_code, string code_mobile);
        List<Tsettings> GetMobileDBsettings(string app_code, string code_mobile);
        List<Tstyles> GetMobileDBstyles(string app_code, string code_mobile);
        List<Tpredpr_kart> GetMobileDBpredpr_kart(string app_code, string code_mobile);
        List<Tpredpr_tov_art> GetMobileDBpredpr_tov_art(string app_code, string code_mobile);
        List<Tpredpr_tov_gr> GetMobileDBpredpr_tov_gr(string app_code, string code_mobile);
        List<Ttov_gr_tov_art> GetMobileDBtov_gr_tov_art(string app_code, string code_mobile);
        List<Tquestions> GetMobileDBquestions(string app_code, string code_mobile);
        List<Tanswers> GetMobileDBanswers(string app_code, string code_mobile);
    }
}