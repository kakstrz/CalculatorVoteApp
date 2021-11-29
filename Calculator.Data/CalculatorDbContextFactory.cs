using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Calculator.Data
{
    public class CalculatorDbContextFactory : IDesignTimeDbContextFactory<CalculatorDbContext>
    {
        public CalculatorDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<CalculatorDbContext>();
            options.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=Calculator; Integrated Security=True");

            return new CalculatorDbContext(options.Options);
        }
    }
}
