using MobileApi.Data.Interfaces;
using MobileApi.Data.Models;
using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Interfaces;
using MobileApi.Logic.Models;
using System.Collections.Generic;

namespace MobileApi.Logic.Services
{
    public class BaseService : IBaseService
    {
        private readonly IBaseRepository repo;

        public BaseService(IBaseRepository repository)
        {
            repo = repository;
        }

        public List<TGetNewTablesMobileDB> GetNewTablesMobileDB(string app_code, string code_mobile)
        {
            return repo.GetNewTablesMobileDB(app_code, code_mobile);
        }

        public List<TGetNewTablesMobileDB> GetAllTablesMobileDB(string app_code, string code_mobile)
        {            
            return repo.GetAllTablesMobileDB(app_code, code_mobile);
        }

        public TResult UpdateBase(string app_code, List<Table> tables)
        {
            var resultStr = "";
            foreach (Table table in tables)
            {
                var tableName = table.tableName;
                resultStr += tableName;
                resultStr += UpdateTable(tableName, app_code, table);
            }

            var result = new TResult() { type = "success", value = "" };
            return result;
        }

        public string UpdateTable(string tableName, string appCode, Table table)
        {
            var result = "";

            switch (tableName)
            {
                case "predpr":
                    foreach (Tpredpr row in table.predpr)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "tov_gr":
                    foreach (Ttov_gr row in table.tov_gr)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "tov_art":
                    foreach (Ttov_art row in table.tov_art)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "tov_img":
                    foreach (Ttov_img row in table.tov_img)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "predpr_kart":
                    foreach (Tpredpr_kart row in table.predpr_kart)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "predpr_tov_gr":
                    foreach (Tpredpr_tov_gr row in table.predpr_tov_gr)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "predpr_tov_art":
                    foreach (Tpredpr_tov_art row in table.predpr_tov_art)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "tov_gr_tov_art":
                    foreach (Ttov_gr_tov_art row in table.tov_gr_tov_art)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "settings":
                    foreach (Tsettings row in table.settings)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "tov_contacts":
                    foreach (Ttov_contacts row in table.tov_contacts)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "styles":
                    foreach (Tstyles row in table.styles)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "questions":
                    foreach (Tquestions row in table.questions)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                case "answers":
                    foreach (Tanswers row in table.answers)
                    {
                        result += repo.UpdateRow(tableName, appCode, row);
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
        
        public TResult DeleteFromBase(string app_code, List<TRowToDelete> rowsToDelete)
        {
            var resultStr = "";
            foreach (TRowToDelete rtd in rowsToDelete)
            {
                resultStr += repo.DeleteRow(app_code, rtd);
            }

            var result = new TResult() { type = "success", value = "" };
            return result;
        }
    }
}