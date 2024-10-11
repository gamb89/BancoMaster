
using BancoMasterTest.Domain.Interfaces;
using BancoMasterTest.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Runtime.InteropServices;

namespace BancoMasterTest.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var route = String.Empty;
            var services = new ServiceCollection();
          //  ConfigureServices(services);

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            var serviceProvider = new ServiceCollection()
                 .AddSingleton<IConfiguration>(configuration)
                 .AddScoped<IRouteService, RouteService>()
                 .BuildServiceProvider();

            var service = serviceProvider.GetService<IRouteService>();

            while (true)
            {

                System.Console.WriteLine("Digite a rota desejada no formato 'DE-PARA' ");
                string inputRoute = System.Console.ReadLine().ToUpper();

                var isValid = service.Validate(inputRoute);

                if (!isValid)
                {
                    System.Console.WriteLine("Valor inválido. Vamos tentar novamente.");
                }
                else
                {
                    var result = await service.GetRoute(inputRoute);
                    System.Console.WriteLine(result.Message);

                }
                    break;
            }
        }


    }
}
