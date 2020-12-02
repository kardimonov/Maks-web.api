using MobileApi.Data.Interfaces;
using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Interfaces;
using MobileApi.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MobileApi.Logic.Services
{
    public class VariaService : IVariaService
    {
        private readonly IVariaRepository repo;
        private readonly IPictureService service;

        public VariaService(IVariaRepository repository, IPictureService serv)
        {
            repo = repository;
            service = serv;
        }

        public int ServerTest()
        {
            var result = 0;            
            var dataTable = repo.ServerTest();

            foreach (DataRow row in dataTable.Rows)
            {
                result = Convert.ToInt32(row["Mark"]);
                result += 1;
            }
            return 1 / result;
        }

        public TCheckMobileAppCodeUrl CheckMobileAppCodeUrl(string app_code, string url)
        {
            var result = repo.CheckMobileAppCodeUrl(app_code, url);
            var resultCanEdit = repo.CheckPasswordMobileChief(app_code, url, result);            

            var canEdit = false;
            if ((resultCanEdit > 0) && (resultCanEdit == result))
            {
                canEdit = true;
            }

            return new TCheckMobileAppCodeUrl()
            {
                Result = result,
                CanEdit = canEdit
            };
        }

        public TUpdateMarkMobileDB UpdateMarkMobileDB(string app_code, string code_mobile, string tablename)
        {
            repo.UpdateMarkMobileDB(app_code, code_mobile, tablename);

            return new TUpdateMarkMobileDB()
            {
                Result = true,
            };
        }

        public TGetMobileShowLogin GetMobileShowLogin(string app_code)
        {
            var message = repo.GetMobileShowLogin(app_code);
            
            return new TGetMobileShowLogin()
            {
                Result = message,
            };
        }

        public TResult SetUpdateAllFlag(string app_code)
        {
            var result = new TResult() { type = "error", value = "" };            
            var dataTable = repo.SetUpdateAllFlag(app_code);

            foreach (DataRow row in dataTable.Rows)
            {
                var resultString = row["ExecSql"].ToString();
                if (resultString.Equals("0", StringComparison.Ordinal))
                {
                    result.type = "success";
                }
                else if (resultString.Equals("1", StringComparison.Ordinal))
                {
                    result.value = "locked";
                }
            }
            return result;
        }

        public List<Tpredpr> GetMobileDBpredpr(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "predpr", code_mobile);
            var result = new List<Tpredpr>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tpredpr()
                {
                    p_id = row["p_id"].ToString(),
                    p_name = row["p_name"].ToString(),
                    p_catalog = row["p_catalog"].ToString(),
                    p_image = row["p_image"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_gr> GetMobileDBtov_gr(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_gr", code_mobile);
            var result = new List<Ttov_gr>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_gr()
                {
                    p_id = row["p_id"].ToString(),
                    g_id = row["g_id"].ToString(),
                    g_name = row["g_name"].ToString(),
                    g_image = row["g_image"].ToString(),
                    g_sup = row["g_sup"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_art> GetMobileDBtov_art(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_art", code_mobile);
            var result = new List<Ttov_art>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_art()
                {
                    g_id = row["g_id"].ToString(),
                    t_article = row["t_article"].ToString(),
                    t_name = row["t_name"].ToString(),
                    t_cost = row["t_cost"].ToString(),
                    fl_mark = Convert.ToInt32(row["fl_mark"]),
                    ctlg_id = row["ctlg_id"].ToString(),
                    t_description = row["t_description"].ToString(),
                    t_image = row["t_image"].ToString(),
                    t_namebasket = row["t_namebasket"].ToString(),
                    t_imageprev = row["t_imageprev"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_img> GetMobileDBtov_img(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_img", code_mobile);
            var result = new List<Ttov_img>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_img()
                {
                    filename = row["filename"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_img> GetMobileDBtov_img_async(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_img_async", code_mobile);
            var result = new List<Ttov_img>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_img()
                {
                    filename = row["filename"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_img_base64> GetMobileDBtov_img_base64_async(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_img_async", code_mobile);
            var result = new List<Ttov_img_base64>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_img_base64()
                {
                    filename = row["filename"].ToString(),
                    imgBase64 = Convert.ToBase64String(ReadFully(
                        service.GetImageMobile(app_code, row["filename"].ToString())
                        )),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_img> GetMobileDBtov_img_sync(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_img_sync", code_mobile);
            var result = new List<Ttov_img>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_img()
                {
                    filename = row["filename"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_contacts> GetMobileDBtov_contacts(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_contacts", code_mobile);
            var result = new List<Ttov_contacts>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_contacts()
                {
                    t_article = row["t_article"].ToString(),
                    phone = row["phone"].ToString(),
                    email = row["email"].ToString(),
                    newappcode = row["newappcode"].ToString(),
                    latitude = row["latitude"].ToString(),
                    longitude = row["longitude"].ToString(),
                    mapOfflineRadius = row["mapOfflineRadius"].ToString(),
                    mapOfflineZoomMin = Convert.ToInt32(row["mapOfflineZoomMin"]),
                    mapOfflineZoomMax = Convert.ToInt32(row["mapOfflineZoomMax"]),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tctlg> GetMobileDBctlg(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "ctlg", code_mobile);
            var result = new List<Tctlg>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tctlg()
                {
                    ctlg_id = row["ctlg_id"].ToString(),
                    ctlg_name = row["ctlg_name"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tsettings> GetMobileDBsettings(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "settings", code_mobile);
            var result = new List<Tsettings>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tsettings()
                {
                    name = row["name"].ToString(),
                    value = row["value"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tquestions> GetMobileDBquestions(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "questions", code_mobile);
            var result = new List<Tquestions>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tquestions()
                {
                    q_id = row["q_id"].ToString(),
                    q_description = row["q_description"].ToString(),
                    q_rightansid = row["q_rightansid"].ToString(),
                    q_image = row["q_image"].ToString(),
                    q_anstext = row["q_anstext"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tanswers> GetMobileDBanswers(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "answers", code_mobile);
            var result = new List<Tanswers>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tanswers()
                {
                    a_id = row["a_id"].ToString(),
                    a_description = row["a_description"].ToString(),
                    q_id = row["q_id"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tstyles> GetMobileDBstyles(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "styles", code_mobile);
            var result = new List<Tstyles>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tstyles()
                {
                    element = row["element"].ToString(),
                    property = row["property"].ToString(),
                    value = row["value"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tpredpr_kart> GetMobileDBpredpr_kart(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "predpr_kart", code_mobile);
            var result = new List<Tpredpr_kart>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tpredpr_kart()
                {
                    p_id = row["p_id"].ToString(),
                    p_descr = row["p_descr"].ToString(),
                    p_image = row["p_image"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tpredpr_tov_art> GetMobileDBpredpr_tov_art(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "predpr_tov_art", code_mobile);
            var result = new List<Tpredpr_tov_art>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tpredpr_tov_art()
                {
                    p_id = row["p_id"].ToString(),
                    t_article = row["t_article"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Tpredpr_tov_gr> GetMobileDBpredpr_tov_gr(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "predpr_tov_gr", code_mobile);
            var result = new List<Tpredpr_tov_gr>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Tpredpr_tov_gr()
                {
                    p_id = row["p_id"].ToString(),
                    g_id = row["g_id"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<Ttov_gr_tov_art> GetMobileDBtov_gr_tov_art(string app_code, string code_mobile)
        {
            var dataTable = repo.GetDataTable(app_code, "tov_gr_tov_art", code_mobile);
            var result = new List<Ttov_gr_tov_art>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new Ttov_gr_tov_art()
                {
                    g_id = row["g_id"].ToString(),
                    t_article = row["t_article"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}