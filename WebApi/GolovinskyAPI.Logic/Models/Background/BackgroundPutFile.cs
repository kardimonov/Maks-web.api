using Microsoft.AspNetCore.Http;

namespace GolovinskyAPI.Logic.Models.Background
{
    public class BackgroundPutFile : BackgroundBase
    {
        public IFormFile Image { get; set; }

        public char Orientation { get; set; }

        public char Place { get; set; }
    }
}