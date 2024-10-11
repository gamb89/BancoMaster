using AutoBogus;
using BancoMasterTest.Domain.Model;
using BancoMasterTest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoMasterTest.Tests.Fixture
{
    public class RouteServiceTestsFixture : BaseTestsFixture
    {
        public Route ObjectToAdd; 
        public RouteService GetRouteService()
        {
            Mocker = new Moq.AutoMock.AutoMocker();

            ObjectToAdd = GenerateValidRoute();

            return Mocker.CreateInstance<RouteService>();
        }

        public static Route GenerateValidRoute()
        {
            return new AutoFaker<Route>("pt_BR")
                .RuleFor(x => x.Destiny, (f, c) => "SCL")
                .RuleFor(x => x.Origin, (f, c) => "GRU")
                .Generate();
        }
    }
}
