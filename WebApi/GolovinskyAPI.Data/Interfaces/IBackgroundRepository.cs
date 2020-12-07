using GolovinskyAPI.Data.Models.Background;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolovinskyAPI.Data.Interfaces
{
    public interface IBackgroundRepository : IBaseRepository
    {
        Task<string> CreateAsync(Background background);
        Task<List<Background>> GetBackgroundAsync(Background background);
        Task<string> UpdateAsync(Background background);
        Task<string> DeleteAsync(Background background);
    }
}