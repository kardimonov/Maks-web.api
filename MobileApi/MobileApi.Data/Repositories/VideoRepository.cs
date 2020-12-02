using Microsoft.Data.SqlClient;
using MobileApi.Data.Interfaces;
using System.Data;
using System.IO;

namespace MobileApi.Data.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        public byte[] GetVideoMobile(string app_code, string img_filename)
        {
            byte[] data = null;
            using (var connection = new SqlConnection(Global.Connection))
            {
                connection.Open();
                var command = new SqlCommand
                    ($"sp_GetImageMobile @AppCode='{app_code}', @img_filename='{img_filename}'", connection);

                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                reader.Read();

                using (var memory = new MemoryStream())
                {
                    long startIndex = 0;
                    const int ChunkSize = 256;
                    while (true)
                    {
                        var buffer = new byte[ChunkSize];
                        long retrievedBytes = reader.GetBytes(0, startIndex, buffer, 0, ChunkSize);
                        memory.Write(buffer, 0, (int)retrievedBytes);
                        startIndex += retrievedBytes;
                        if (retrievedBytes != ChunkSize)
                        {
                            break;
                        }
                    }
                    data = memory.ToArray();
                    connection.Close();
                }                
            }
            return data;
        }        

        public byte[] RetrieveFile(string filename, string app_code)
        {
            byte[] data = null;
            using (var connection = new SqlConnection(Global.Connection))
            {
                var command = new SqlCommand
                    ($"sp_GetImageMobile @AppCode='{app_code}', @img_filename='{filename}'", connection);
                //command.Parameters.AddWithValue("@Filename", filename);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                reader.Read();

                using (var memory = new MemoryStream())
                {
                    long startIndex = 0;
                    const int ChunkSize = 256;
                    while (true)
                    {
                        var buffer = new byte[ChunkSize];
                        long retrievedBytes = reader.GetBytes(0, startIndex, buffer, 0, ChunkSize);
                        memory.Write(buffer, 0, (int)retrievedBytes);
                        startIndex += retrievedBytes;

                        if (retrievedBytes != ChunkSize)
                        {
                            break;
                        }
                    }
                    data = memory.ToArray();
                }
                connection.Close();
            }
            return data;
        }
    }
}