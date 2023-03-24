using BeMo.Models;
using BeMo.Options;
using BeMo.Repositories;
using BeMo.Repositories.Interfaces;
using BeMo.Services;

namespace BeMo.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDependencyServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<StravaOptions>(builder?.Configuration.GetSection("StravaConfig"));

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Challenge>, ChallengeRepository>();
            services.AddScoped<IRepository<Activity>, ActivityRepository>();

            services.AddHostedService<RefreshStravaTokensRepeatingService>();
            services.AddHostedService<RetrieveStravaDataRepeatingService>();
        }
    }
}
