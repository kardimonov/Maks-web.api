using GolovinskyAPI.Models.Catalog;

namespace GolovinskyAPI.Infrastructure.Interfaces
{
    public interface ICatalogRepository
    {
        CatalogOutput Create(Catalog catalog);
        CatalogOutput Update(Catalog catalog);
        string Delete(Catalog catalog);
    }
}
