using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoMasterTest.Domain.Model
{
    public class Route
    {
        public string Origin { get; set; }

        public string Destiny { get; set; }

        public decimal Price { get; set; }

        public Route()
        {
            Origin = String.Empty;
            Destiny = String.Empty;
        }

        public Route(string destiny, decimal? price)
        {
            Origin = String.Empty;
            Destiny = destiny;
            Price = price ?? 0 ;
        }


    }


}
