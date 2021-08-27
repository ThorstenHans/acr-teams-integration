using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Thns.Functions.Startup))]
namespace Thns.Functions
{

    public class Startup: FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<TeamsConfig>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("ContainerRegistryTeams").Bind(settings);
                });
        }

    }
}
