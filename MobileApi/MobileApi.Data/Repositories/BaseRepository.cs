using Microsoft.Data.SqlClient;
using MobileApi.Data.Interfaces;
using MobileApi.Data.Models;
using MobileApi.Data.Models.InnerClasses;
using System.Collections.Generic;
using System.Data;

namespace MobileApi.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public List<TGetNewTablesMobileDB> GetNewTablesMobileDB(string app_code, string code_mobile)
        {
            var strSQL = $"sp_GetNewTablesMobileDB @AppCode='{app_code}', @CodeMobile='{code_mobile}'";
            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);

            var dataTable = dataSet.Tables[0];
            var result = new List<TGetNewTablesMobileDB>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new TGetNewTablesMobileDB()
                {
                    t = row["t"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public List<TGetNewTablesMobileDB> GetAllTablesMobileDB(string app_code, string code_mobile)
        {
            var strSQL = $"sp_GetNewTablesMobileDB @AppCode='{app_code}', @CodeMobile='{code_mobile}', @IsAll=1";
            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);

            var dataTable = dataSet.Tables[0];
            var result = new List<TGetNewTablesMobileDB>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new TGetNewTablesMobileDB()
                {
                    t = row["t"].ToString(),
                };
                result.Add(item);
            }
            return result;
        }

        public string UpdateRow(string tableName, string appCode, object row)
        {
            var result = "";
            using (var connection = new SqlConnection(Global.Connection))
            {
                var command = new SqlCommand($"sp_zMobile_{tableName}", connection);
                command.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = command.Parameters.Add("@appcode", SqlDbType.VarChar, 10);
                param.Value = appCode;

                var myType = row.GetType();
                var myPropertyInfo = myType.GetProperties();
                for (var i = 0; i < myPropertyInfo.Length; i++)
                {
                    var propValue = myPropertyInfo[i];
                    result += propValue.Name;

                    if (propValue.PropertyType == typeof(string))
                    {
                        result += (string)propValue.GetValue(row, null);
                    }
                    else
                    {
                        result += (string)propValue.GetValue(row, null).ToString();
                    }

                    if (propValue.GetValue(row, null) != null)
                    {
                        if (propValue.PropertyType == typeof(string))
                        {
                            param = command.Parameters.Add($"@{propValue.Name}", SqlDbType.NVarChar, 4000);
                            param.Value = (string)propValue.GetValue(row, null);
                        }
                        if (propValue.PropertyType == typeof(int))
                        {
                            param = command.Parameters.Add($"@{propValue.Name}", SqlDbType.Int);
                            param.Value = (int)propValue.GetValue(row, null);
                        }
                        if (propValue.PropertyType == typeof(float))
                        {
                            param = command.Parameters.Add($"@{propValue.Name}", SqlDbType.Real);
                            param.Value = (float)propValue.GetValue(row, null);
                        }
                        if (propValue.PropertyType == typeof(byte[]))
                        {
                            param = command.Parameters.Add($"@{propValue.Name}", SqlDbType.Binary);
                            param.Value = (byte[])propValue.GetValue(row, null);
                        }
                    }
                    else
                    {
                        param = command.Parameters.Add($"@{propValue.Name}", SqlDbType.Variant);
                        param.Value = null;
                    }
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        public string DeleteRow(string appCode, TRowToDelete rtd)
        {
            var result = "";
            using (var connection = new SqlConnection(Global.Connection))
            {
                connection.Open();
                var command = new SqlCommand($"sp_zMobile_{rtd.tableName}", connection);
                command.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = command.Parameters.Add("@appcode", SqlDbType.VarChar, 10);
                param.Value = appCode;
                param = command.Parameters.Add($"@{rtd.keyName}", SqlDbType.NVarChar, 4000);
                param.Value = rtd.keyValue;

                if (rtd.key2Name.Length > 0)
                {
                    param = command.Parameters.Add($"@{rtd.key2Name}", SqlDbType.NVarChar, 4000);
                    param.Value = rtd.key2Value;
                }
                param = command.Parameters.Add("@del", SqlDbType.Bit);
                param.Value = 1;
                
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
    }
}