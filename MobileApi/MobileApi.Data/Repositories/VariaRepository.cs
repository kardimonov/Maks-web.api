using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using MobileApi.Data.Interfaces;

namespace MobileApi.Data.Repositories
{
    public class VariaRepository : IVariaRepository
    {
        public DataTable ServerTest()
        {
            var strSQL = "sp_GetClass";
            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);
            var dataTable = dataSet.Tables[0];

            return dataTable;
        }

        public int CheckMobileAppCodeUrl(string app_code, string url)
        {
            var result = 0;
            using (var connection = new SqlConnection(Global.Connection))
            {
                connection.Open();
                var command = new SqlCommand("sp_CheckMobileAppCodeUrl", connection);
                command.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = command.Parameters.Add("@AppCode", SqlDbType.VarChar, 10);
                param.Value = app_code;
                param = command.Parameters.Add("@URL", SqlDbType.VarChar, 80);
                param.Value = url;

                command.Parameters.Add("@Result", SqlDbType.Int);
                command.Parameters["@Result"].Direction = ParameterDirection.Output;
                
                command.ExecuteNonQuery();
                result = (int)command.Parameters["@Result"].Value;
                connection.Close();
            }
            return result;
        }

        public int CheckPasswordMobileChief(string app_code, string url, int result)
        {
            var resultCanEdit = 0;
            using (var conn = new SqlConnection(Global.Connection))
            {
                conn.Open();
                var command = new SqlCommand("sp_CheckPasswordMobileChief", conn);
                command.CommandType = CommandType.StoredProcedure;
                var param = new SqlParameter();
                param = command.Parameters.Add("@AppCode", SqlDbType.VarChar, 10);
                param.Value = result.ToString();
                param = command.Parameters.Add("@UserName", SqlDbType.VarChar, 50);
                param.Value = app_code;
                param = command.Parameters.Add("@Password", SqlDbType.VarChar, 50);
                param.Value = url;
                command.Parameters.Add("@Result", SqlDbType.Int);
                command.Parameters["@Result"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                resultCanEdit = (int)command.Parameters["@Result"].Value;
                conn.Close();
            }
            return resultCanEdit;
        }

        public void UpdateMarkMobileDB(string app_code, string code_mobile, string tablename)
        {            
            using (var connection = new SqlConnection(Global.Connection))
            {
                connection.Open();
                var command = new SqlCommand("sp_UpdateMarkMobileDB", connection);
                command.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = command.Parameters.Add("@AppCode", SqlDbType.VarChar, 10);
                param.Value = app_code;
                param = command.Parameters.Add("@CodeMobile", SqlDbType.VarChar, 10);
                param.Value = code_mobile;
                param = command.Parameters.Add("@TableName", SqlDbType.VarChar, 20);
                param.Value = tablename;
                param = command.Parameters.Add("@Mark", SqlDbType.VarChar, 1);
                param.Value = "1";

                command.ExecuteNonQuery();

                var updateMarkMobileDB = new DataSet();
                var adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(updateMarkMobileDB);
                connection.Close();
            }
        }

        public string GetMobileShowLogin(string app_code)
        {
            var message = "";            
            using (var connection = new SqlConnection(Global.Connection))
            {
                connection.Open();
                var command = new SqlCommand("sp_GetMobileShowLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                                
                var param = new SqlParameter();
                param = command.Parameters.Add("@AppCodeMain", SqlDbType.VarChar, 10);
                param.Value = app_code;
                command.Parameters.Add("@ShowLogin", SqlDbType.VarChar, 10);
                command.Parameters["@ShowLogin"].Direction = ParameterDirection.Output;
                
                command.ExecuteNonQuery();

                var getMobileShowLogin = new DataSet();
                var adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;                
                adapter.Fill(getMobileShowLogin);
                message = command.Parameters["@ShowLogin"].Value.ToString();
                connection.Close();
            }
            return message;
        }

        public DataTable SetUpdateAllFlag(string app_code)
        {
            var strSQL = $"sp_zMobile_updateall @appcode='{app_code}'";
            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);
            var dataTable = dataSet.Tables[0];
            
            return dataTable;
        }

        public Stream GetImageMobile(string app_code, string img_filename)
        {
            byte[] data = null;
            using (var connection = new SqlConnection(Global.Connection))
            {
                var command = new SqlCommand
                    ($"sp_GetImageMobile @AppCode='{app_code}', @img_filename='{img_filename}'", connection);
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                using (var memory = new MemoryStream())
                {
                    if (reader.Read())
                    {
                        long startIndex = 0;
                        const int ChunkSize = 256;
                        while (true)
                        {
                            var buffer = new byte[ChunkSize];
                            if (reader.IsDBNull(0))
                            {
                                break;
                            }

                            long retrievedBytes = reader.GetBytes(0, startIndex, buffer, 0, ChunkSize);
                            memory.Write(buffer, 0, (int)retrievedBytes);
                            startIndex += retrievedBytes;

                            if (retrievedBytes != ChunkSize)
                            {
                                break;
                            }                                
                        }
                    }
                    data = memory.ToArray();
                }
                connection.Close();
            }

            return new MemoryStream(data)
            {
                Position = 0,
            };
        }

        public DataTable GetDataTable(string app_code, string tablename, string code_mobile)
        {
            string strSQL;
            if (tablename.Equals("tov_img_async", StringComparison.Ordinal))
            {
                strSQL = $"sp_GetMobileDB @AppCode='{app_code}', @tablename='tov_img', @CodeMobile='{code_mobile}', @IsFirstImages=2";
            }
            else if (tablename.Equals("tov_img_sync", StringComparison.Ordinal))
            {
                strSQL = $"sp_GetMobileDB @AppCode='{app_code}', @tablename='tov_img', @CodeMobile='{code_mobile}', @IsFirstImages=1";
            }
            else
            {
                strSQL = $"sp_GetMobileDB @AppCode='{app_code}', @tablename='{tablename}'";
            }

            var dataSet = new DataSet();
            var adapter = new SqlDataAdapter(strSQL, Global.Connection);
            adapter.Fill(dataSet);
            var dataTable = dataSet.Tables[0];

            return dataTable;
        }
    }
}