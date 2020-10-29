using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolovinskyAPI.Models.ViewModels.Gallery
{
    public class GaleryViewModel
    {
        public List<SearchPictureOutputModel> Images { get; set; }

        public int TotalItems { get; set; }
    }
}
