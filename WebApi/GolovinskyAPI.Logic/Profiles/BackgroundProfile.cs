using AutoMapper;
using GolovinskyAPI.Data.Models.Background;
using GolovinskyAPI.Logic.Models.Background;
using System;

namespace GolovinskyAPI.Logic.Profiles
{
    public class BackgroundProfile : Profile
    {
        public BackgroundProfile()
        {
            CreateMap<(char place, string appCode, char mark, char orientation), Background>()
                .ForMember(b => b.Place, opt => opt.MapFrom(t => t.place))
                .ForMember(b => b.AppCode, opt => opt.MapFrom(t => t.appCode))
                .ForMember(b => b.Mark, opt => opt.MapFrom(t => t.mark))
                .ForMember(b => b.Orient, opt => opt.MapFrom(t => t.orientation))
                .ForMember(b => b.Date, opt => opt.MapFrom(t => DateTime.Now));

            CreateMap<BackgroundPostBase64, Background>()
                .ForMember(b => b.Orient, opt => opt.MapFrom(bpb => bpb.Orientation))
                .ForMember(b => b.Date, opt => opt.MapFrom(bpb => DateTime.Now))
                .ForMember(b => b.Img, opt => opt.MapFrom((src, dest, _, context) => context.Options.Items["Img"]));

            CreateMap<BackgroundPutBase64, Background>()
                .ForMember(b => b.Orient, opt => opt.MapFrom(bpb => bpb.Orientation))
                .ForMember(b => b.Date, opt => opt.MapFrom(bpb => DateTime.Now))
                .ForMember(b => b.Img, opt => opt.MapFrom((src, dest, _, context) => context.Options.Items["Img"]));

            CreateMap<BackgroundPostFile, Background>()
                .ForMember(b => b.Orient, opt => opt.MapFrom(bpb => bpb.Orientation))
                .ForMember(b => b.Date, opt => opt.MapFrom(bpb => DateTime.Now))
                .ForMember(b => b.Img, opt => opt.MapFrom((src, dest, _, context) => context.Options.Items["Img"]));

            CreateMap<BackgroundPutFile, Background>()
                .ForMember(b => b.Orient, opt => opt.MapFrom(bpb => bpb.Orientation))
                .ForMember(b => b.Date, opt => opt.MapFrom(bpb => DateTime.Now))
                .ForMember(b => b.Img, opt => opt.MapFrom((src, dest, _, context) => context.Options.Items["Img"]));

            CreateMap<BackgroundBase64Delete, Background>();
        }
    }
}