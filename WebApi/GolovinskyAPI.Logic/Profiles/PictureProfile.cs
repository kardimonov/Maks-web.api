using AutoMapper;
using GolovinskyAPI.Data.Models.Images;

namespace GolovinskyAPI.Logic.Profiles
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<NewUploadImageInput, NewUploadImageInputByte>()
                .ForMember(nuiib => nuiib.Img, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Img"]));
        }
    }
}