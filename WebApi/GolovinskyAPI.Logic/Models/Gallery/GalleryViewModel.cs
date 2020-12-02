using GolovinskyAPI.Data.Models;
using System.Collections.Generic;

namespace GolovinskyAPI.Logic.Models.Gallery
{
    public class GalleryViewModel
    {
        public List<SearchPictureOutputModel> Images { get; set; }
        public int TotalItems { get; set; }
    }
}