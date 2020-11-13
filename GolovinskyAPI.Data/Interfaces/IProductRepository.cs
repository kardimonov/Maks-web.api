using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IProductRepository : IBaseRepository
    {
        NewProductOutputModel InsertProduct(NewProductInputModel input);
        bool UpdateProduct(NewProductInputModel input);
        bool DeleteProduct(DeleteProductInputModel input);
        List<SearchPictureOutputModel> SearchProduct(SearchPictureInputModel input);
    }
}