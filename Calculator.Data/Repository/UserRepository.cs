using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CalculatorDbContextFactory context) : base(context) { }

        public async Task<IEnumerable<User>> GetUsers(string name, string surename)
        {
            using (CalculatorDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<User> users = (await context.Set<User>().ToListAsync()).Where(
                    (u) => u.Name == name && u.SureName == surename);

                return users;
            }
        }
    }
}
