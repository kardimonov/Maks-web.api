using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Background;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolovinskyAPI.Logic.Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IBackgroundRepository _repo;
        private readonly IUploadPicture _uploadHandler;

        public BackgroundService(IBackgroundRepository repo, IUploadPicture uploadHandler)
        {
            _repo = repo;
            _uploadHandler = uploadHandler;
        }

        public async Task<List<Background>> GetBackgroundAsync(Background background)
        {
            var result = await _repo.GetBackgroundAsync(background);
            return result;
        }

        public async Task<ResponseViewModel> CreateAsync(Background background)
        {           
            var response = await _repo.CreateAsync(background);
            return new ResponseViewModel { Response = response };
        }

        public async Task<ResponseViewModel> UpdateAsync(Background background)
        {
            var response = await _repo.UpdateAsync(background);
            return new ResponseViewModel { Response = response };
        }

        public async Task<ResponseViewModel> DeleteAsync(Background background)
        {            
            var response = await _repo.DeleteAsync(background);

            return response == "1" ? 
                new ResponseViewModel { Response = response } : 
                new ResponseViewModel { Response = "Ошибка выполнения процедуры" };
        }
    }
}