using Bogus;
using Microsoft.Extensions.Configuration;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BancoMasterTest.Tests.Fixture
{
    public class BaseTestsFixture
    {
        public AutoMocker Mocker; 
        public static Faker Faker { get; private set; }

        protected void SetupAppSettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", optional:false, reloadOnChange: true)
                .Build();

            Mocker.Use<IConfiguration>(configuration);
        }
    }
}
