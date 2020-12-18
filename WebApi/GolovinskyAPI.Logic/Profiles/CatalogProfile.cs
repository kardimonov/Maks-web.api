using AutoMapper;
using GolovinskyAPI.Data.Models.Catalog;
using GolovinskyAPI.Logic.Models.Catalog;

namespace GolovinskyAPI.Logic.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CreateCatalog, Catalog>();
            CreateMap<EditCatalog, Catalog>();
            CreateMap<DeleteCatalog, Catalog>();
        }
    }
}