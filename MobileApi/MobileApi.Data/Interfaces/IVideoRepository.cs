namespace MobileApi.Data.Interfaces
{
    public interface IVideoRepository : IRepository
    {
        byte[] GetVideoMobile(string app_code, string img_filename);
        byte[] RetrieveFile(string filename, string app_code);
    }
}