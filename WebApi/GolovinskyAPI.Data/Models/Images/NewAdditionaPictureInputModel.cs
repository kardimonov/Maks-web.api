namespace GolovinskyAPI.Data.Models.Images
{
    public class NewAdditionalPictureInputModel
    {
        // id пользователя
        public string Catalog { get; set; }
        // id категории
        public string Id { get; set; }
        // id товара
        public int Prc_ID { get; set; }
        // порядковый номер картинки в ряду
        public string ImageOrder { get; set; }
        // наименование картинки
        public string TImage { get; set; }
        // id магазина
        public string Appcode { get; set; }
        // id пользователя, должен будет передаваться при работе Системы частных объявлений
        public int CID { get; set; }
    }
}