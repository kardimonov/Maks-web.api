namespace MobileApi.Data.Interfaces
{
    public interface IPictureRepository : IRepository
    {
        byte[] GetImageMobile(string app_code, string img_filename);
        byte[] GetImageMobileBase64(string app_code, string img_filename);
        string GetImageMobile1(string app_code, string img_filename);
        void UploadPhoto(string appCode, string fileName, byte[] img);
    }
}