namespace GolovinskyAPI.Data.Models.Images
{
    public class NewUploadImageInputByte
    {
        // id магазина
        public string AppCode { get; set; }
        // наименование картинки
        public string TImageprev { get; set; }
        // сама картинка
        public byte[] Img { get; set; }
    }
}