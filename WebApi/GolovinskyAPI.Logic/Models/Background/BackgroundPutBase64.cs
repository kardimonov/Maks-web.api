namespace GolovinskyAPI.Logic.Models.Background
{
    public class BackgroundPutBase64
    {
        public string AppCode { get; set; }

        public string FileName { get; set; }

        public string Image { get; set; }

        public char Orientation { get; set; }

        public char Place { get; set; }
    }
}