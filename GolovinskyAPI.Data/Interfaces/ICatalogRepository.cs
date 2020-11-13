using GolovinskyAPI.Data.Models.Catalog;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface ICatalogRepository : IBaseRepository
    {
        CatalogOutput Create(Catalog catalog);
        CatalogOutput Update(Catalog catalog);
        string Delete(Catalog catalog);
    }
}