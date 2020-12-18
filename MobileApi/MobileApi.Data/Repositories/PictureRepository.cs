using Microsoft.Data.SqlClient;
using MobileApi.Data.Interfaces;
using System;
using System.Data;
using System.IO;

namespace MobileApi.Data.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        public byte[] GetImageMobile(string app_code, string img_filename)
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
            return data;
        }

        public byte[] GetImageMobileBase64(string app_code, string img_filename)
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
                            var retrievedBytes = reader.GetBytes(0, startIndex, buffer, 0, ChunkSize);
                            memory.Write(buffer, 0, (int)retrievedBytes);
                            startIndex += retrievedBytes;

                            if (retrievedBytes != ChunkSize)
                            {
                                break;
                            }
                        }
                    }
                    data = memory.ToArray();
                    connection.Close();
                }
            }
            return data;
        }

        public string GetImageMobile1(string app_code, string img_filename)
        {
            var message = "q";
            using (var connection = new SqlConnection(Global.Connection))
            {
                connection.Open();
                var command = new SqlCommand
                    ($"sp_GetImageMobile @AppCode='{app_code}', @img_filename='{img_filename}'", connection);

                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                reader.Read();

                using (var memory = new MemoryStream())
                {
                    long retrievedBytes;
                    long startIndex = 0;
                    const int ChunkSize = 256;

                    var buffer = new byte[ChunkSize];
                    try
                    {
                        retrievedBytes = reader.GetBytes(0, startIndex, buffer, 0, ChunkSize);
                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                        retrievedBytes = 0;
                    }
                    memory.Write(buffer, 0, (int)retrievedBytes);

                    startIndex += retrievedBytes;
                }
                connection.Close();
            }
            return message;
        }

        public void UploadPhoto(string appCode, string fileName, byte[] img)
        {
            using (var connection = new SqlConnection(Global.Connection))
            {
                var command = new SqlCommand("sp_zMobile_tov_img", connection);
                command.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = command.Parameters.Add("@appcode", SqlDbType.VarChar, 10);
                param.Value = appCode;
                param = command.Parameters.Add("@filename", SqlDbType.VarChar, 4000);
                param.Value = fileName;
                param = command.Parameters.Add("@img", SqlDbType.Binary);
                param.Value = img;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}