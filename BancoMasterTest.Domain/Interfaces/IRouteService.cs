using BancoMasterTest.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoMasterTest.Domain.Interfaces
{
    public interface IRouteService
    {
        Task<bool> Add(Route route);

        Task<ReturnObject> GetRoute(string route);

        bool Validate(string route);
    }
}
