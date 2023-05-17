using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CensusDemographic.Domain.Interfaces.Repositories;
using CensusDemographic.Domain.Interfaces.Services;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Validators;
using CensusDemographic.Infra.Data.Repositories;
using CensusDemographic.Infra.Setup.Extensions;
using CensusDemographic.Services.Services;

namespace CensusDemographic.Infra.Setup
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureApp(IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);

            services
                .AddMongoClientConfiguration(Configuration)
                .AddStoneContext(Configuration);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<Person>, PersonValidator>();
        }
    }
}