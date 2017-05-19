using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using TP3.DataAccessLayer;

namespace TP3.Tests
{
    public class HedgesContextFactory : IDbContextFactory<HedgesProductionsContext>
    {

        public HedgesProductionsContext Create(DbContextFactoryOptions options)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();

            var optionsBuilder = new DbContextOptionsBuilder<HedgesProductionsContext>();

            optionsBuilder.UseInMemoryDatabase();

            return new HedgesProductionsContext(optionsBuilder.Options);
        }

        public HedgesProductionsContext Create()
        {
            return Create(null);
        }
    }
}
