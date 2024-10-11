using BancoMasterTest.Domain.Interfaces;
using BancoMasterTest.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace BancoMasterTest.Domain.Services
{
    public class RouteService : IRouteService
    {
        private readonly IConfiguration _configuration;

        public RouteService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Validate(string route)
        {
            if (string.IsNullOrEmpty(route) || !route.Contains("-"))
                return false;


            return true;
        }

        #region Add Route
        public async Task<bool> Add(Route route)
        {
            String path = @"C:\file\rotas.csv";

            var lines = new string[] {
               String.Format("{0},{1},{2}", route.Origin, route.Destiny, route.Price.ToString())
            };

            using (var writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(lines.First());
            }

            return true;
        }

        #endregion

        public async Task<ReturnObject> GetRoute(string route)
        {
            try
            {
                Dictionary<string, List<Route>> connections = new();

                if (!this.Validate(route))
                    return new(false, "Rota inválida");

                var availableRoutes = GetRoutesFromFile();

                foreach (var availableRoute in availableRoutes)
                {
                    if (!connections.ContainsKey(availableRoute.Origin))
                    {
                        connections[availableRoute.Origin] = new List<Route>();
                    }
                    connections[availableRoute.Origin].Add(new Route(availableRoute.Destiny, availableRoute.Price));
                }

                var bestRoute = FilterBestRoute(ref connections, route);
                if (bestRoute.Cost == 0)
                {
                    return new(false, "Rota não encontrada");
                }


                return new ReturnObject(true, "Melhor custo: " + bestRoute.Cost.ToString() + " - ROTA: " + String.Join("-", bestRoute.CompleteRoute));
            }
            catch (Exception ex)
            {
                return new(false, "Ocorreu um erro ao pesquisar a rota");
            }
        }

        private List<Route> GetRoutesFromFile()
        {
            var returnList = new List<Route>();

            var filePath = _configuration.GetSection("filePath").Value?.ToString();

            if (String.IsNullOrEmpty(filePath))
                throw new Exception();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    var newRoute = new Route();
                    //Processing row
                    var col = 0;
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        switch (col)
                        {
                            case 0:
                                newRoute.Origin = field;
                                col++;
                                break;
                            case 1:
                                newRoute.Destiny = field;
                                col++;
                                break;
                            case 2:
                                newRoute.Price = Convert.ToDecimal(field);
                                col++;
                                break;
                        }
                    }
                    returnList.Add(newRoute);
                }
            }

            return returnList;
        }
     

        private (decimal Cost, List<string> CompleteRoute) FilterBestRoute(ref Dictionary<string, List<Route>> connections, string clientRoute)
        {
            var origin = clientRoute.Split("-")[0].ToString();
            var destiny = clientRoute.Split("-")[1].ToString();

            var listCost = new Dictionary<string, decimal>();
            var routes = new Dictionary<string, string>();
            var availablesRoutes = new List<string>();

            foreach (var connection in connections.Keys)
            {
                listCost[connection] = decimal.MaxValue;
                availablesRoutes.Add(connection);
            }

            listCost[origin] = 0;

            while (availablesRoutes.Count > 0)
            {
                var selectedRoute = availablesRoutes[0];
                foreach (var route in availablesRoutes)
                {
                    if (listCost[route] < listCost[selectedRoute])
                    {
                        selectedRoute = route;
                    }
                }

                availablesRoutes.Remove(selectedRoute);

                if (selectedRoute == destiny)
                {
                    break;
                }

                if (!connections.ContainsKey(selectedRoute)) continue;

                foreach (var rota in connections[selectedRoute])
                {
                    var novoCusto = listCost[selectedRoute] + rota.Price;
                    if (listCost.ContainsKey(rota.Destiny))
                    {
                        if (novoCusto < listCost[rota.Destiny])
                        {
                            listCost[rota.Destiny] = novoCusto;
                            routes[rota.Destiny] = selectedRoute;
                        }
                    }
                }
            }
          
            var bestRoute = new List<string>();
            for (var route = destiny; route != null; route = routes.ContainsKey(route) ? routes[route] : null)
            {
                bestRoute.Add(route);
            }
            bestRoute.Reverse();

            return (listCost[destiny], bestRoute);


        }
    }
}
