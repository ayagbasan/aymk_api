using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aymk_engine.Model.Custom
{
    public class AlertResponse:AccountResponse
    {
        public Alert Alert { get; set; }
        public CatalogMarket Market { get; set; }
        public CatalogExchange Exchange { get; set; }

    }
}
