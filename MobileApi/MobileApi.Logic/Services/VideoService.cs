using MobileApi.Data.Interfaces;
using MobileApi.Data.Models.InnerClasses;
using MobileApi.Logic.Interfaces;
using System;
using System.IO;

namespace MobileApi.Logic.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository repo;

        public VideoService(IVideoRepository repository)
        {
            repo = repository;
        }

        public Stream GetVideoMobile(string app_code, string img_filename)
        {
            var data = repo.GetVideoMobile(app_code, img_filename);

            return new MemoryStream(data)
            {
                Position = 0,
            };
        }

        public TVideoFileSave VideoFileSave(string app_code, string code_mobile, string filename)
        {
            var videoBytes = repo.RetrieveFile(filename, app_code);
            var result = false;
            var resfilename = $"{code_mobile}_{app_code}_{filename}";
            var errorText = "";

            FileStream writeStream;
            try
            {
                writeStream = new FileStream($"C:\\InetPub\\wwwroot\\video\\{resfilename}", FileMode.Create);
                var writeBinary = new BinaryWriter(writeStream);

                writeBinary.Write(videoBytes);

                writeBinary.Close();
                result = true;
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                resfilename = "";
            }

            return new TVideoFileSave()
            {
                Result = result,
                FileName = resfilename,
                ErrorText = errorText,
            };
        }

        public TVideoFileDelete VideoFileDelete(string app_code, string code_mobile, string filename)
        {
            var resFilename = $"{code_mobile}_{app_code}_{filename}";
            File.Delete($"C:\\InetPub\\wwwroot\\video\\{resFilename}");

            return new TVideoFileDelete()
            {
                Result = true,
            };
        }

        public byte[] RetrieveFile(string filename, string app_code)
        {
            return repo.RetrieveFile(filename, app_code);
        }
    }
}