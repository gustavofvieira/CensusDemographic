using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using CensusDemographic.Infra.Data.Context;

namespace CensusDemographic.Infra.Setup.Extensions
{
    public static class StoneExtensions
    {
        public static IServiceCollection AddStoneContext(this IServiceCollection services, IConfiguration config)
        {
            return services
                 .AddScoped(sp =>
                    new CensusContext(
                        sp.GetRequiredService<MongoClient>().GetDatabase(config.GetConnectionString("StoneCensusDatabase"))
                    )
                );
        }
    }
}
