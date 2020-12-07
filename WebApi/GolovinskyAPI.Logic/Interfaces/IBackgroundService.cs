using GolovinskyAPI.Data.Models.Background;
using GolovinskyAPI.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IBackgroundService : IBaseService
    {
        Task<List<Background>> GetBackgroundAsync(Background background);
        Task<ResponseViewModel> CreateAsync(Background background);
        Task<ResponseViewModel> UpdateAsync(Background background);
        Task<ResponseViewModel> DeleteAsync(Background background);
    }
}