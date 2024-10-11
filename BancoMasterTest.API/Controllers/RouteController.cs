using BancoMasterTest.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace BancoMasterTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Domain.Model.Route route)
        {
            return Ok(await _routeService.Add(route));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string route)
        {
            return Ok(await _routeService.GetRoute(route)); 
        }
    }
}
