using AutoMapper;
using GolovinskyAPI.Data.Models.Catalog;
using GolovinskyAPI.Logic.Models.Catalog;

namespace GolovinskyAPI.Logic.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CreateCatalogViewModel, Catalog>();
            CreateMap<EditCatalogViewModel, Catalog>();
            CreateMap<DeleteCatalogViewModel, Catalog>();
        }
    }
}