using Calculator.Domain.Models;
using System.Threading.Tasks;

namespace Calculator.Domain.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<User> Login(string name, string surename, string pesel);
    }
}
