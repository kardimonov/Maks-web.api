using System.Threading.Tasks;

namespace GolovinskyAPI.Logic.Interfaces
{
    public interface IEmailService : IBaseService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}