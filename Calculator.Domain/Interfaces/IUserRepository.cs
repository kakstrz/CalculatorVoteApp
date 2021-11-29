using Calculator.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculator.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetUsers(string name, string surename);
    }
}
