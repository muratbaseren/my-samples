using System.Threading.Tasks;
using WebApiToken.Models;

namespace WebApiToken.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> IsUserExists(string username);
    }
}
