using Calculator.Domain.Models;
using System.Threading.Tasks;

namespace Calculator.UI.Authentication
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }
        bool Voted { get; }

        Task<bool> Login(string name, string surename, string pesel);
        void Logout();
    }
}

