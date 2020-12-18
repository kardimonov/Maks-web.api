using Microsoft.AspNetCore.Http;

namespace GolovinskyAPI.Logic.Models.Background
{
    public class BackgroundPostFile
    {
        public string AppCode { get; set; }

        public string FileName { get; set; }

        public IFormFile Image { get; set; }

        public char Orientation { get; set; }

        public char Place { get; set; }
    }
}