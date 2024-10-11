using BancoMasterTest.Domain.Services;
using BancoMasterTest.Tests.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BancoMasterTest.Tests.Tests
{
    public class RouteServiceTest : IClassFixture<RouteServiceTestsFixture>
    {
        protected readonly RouteServiceTestsFixture Fixture;
        protected readonly RouteService Service;

        public RouteServiceTest(RouteServiceTestsFixture fixture)
        {
            Fixture = fixture;
            Service = Fixture.GetRouteService();
        }

        #region Add
        [Fact(DisplayName = "Add new Route to CSV")]
        [Trait("Service","RouteService")]
        public async Task Add_WithValidObject_MayReturnSucces()
        {
           var retorno = await Service.Add(Fixture.ObjectToAdd);

            Assert.True(retorno);
        }
        #endregion

        #region Get
        [Fact(DisplayName = "Get Best Route")]
        [Trait("Service", "RouteService")]
        public async Task Get_WithValidRoute_MayReturnSucess()
        {
            const int qtdMessage = 1;
            const string Message = "Melhor custo: 5 - ROTA: BRC-SCL";

            var resultado = await Service.GetRoute("BRC-SCL");

            Assert.True(resultado.Success);
            Assert.Equal(Message, resultado.Message);


        }
        #endregion

    }
}
