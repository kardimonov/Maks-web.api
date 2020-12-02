using AutoMapper;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Models.Admin;

namespace GolovinskyAPI.Logic.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminLoginViewModel, LoginModel>();
            CreateMap<AdminGalleryViewModel, AdminPictureInfo>()
                .ForMember(api => api.SearchDescr, opt => opt.MapFrom(agvm => agvm.Search));
        }
    }
}