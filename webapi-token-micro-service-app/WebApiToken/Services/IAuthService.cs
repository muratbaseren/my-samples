using System.Threading.Tasks;
using WebApiToken.Models;

namespace WebApiToken.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password, string appname);
        Task<User> Login(string username, string password, string appname);
        Task<bool> IsUserExists(string username, string appname);
    }
}
