using Calculator.Domain.Models;
using Calculator.Domain.Models.IndexTypes;
using System.Threading.Tasks;

namespace Calculator.Domain.Interfaces
{
    public interface IUserService
    {
        Task<DisallowedSet> GetDisallowedUsers(UsersIndexType index);
    }
}
