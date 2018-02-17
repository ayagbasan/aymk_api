using ExchangeSharp;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aymk_engine.Model
{
    [BsonIgnoreExtraElements]
    public class CatalogMarket
    {
        public static string collectionName = "catalogmarkets";
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("exchangeId")]
        public ObjectId ExchangeId { get; set; }
        public CatalogExchange Exchange { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("firstCode")]
        public string FirstCode { get; set; }

        [BsonElement("secondCode")]
        public string SecondCode { get; set; }

        [BsonElement("logoUrl")]
        public string LogoUrl { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
 

        [BsonElement("runCount")]
        public int RunCount { get; set; }

        [BsonElement("ticker")]
        public ExchangeTicker Ticker { get; set; } 

        [BsonElement("historicalData")]
        public List<ExchangeTicker> HistoricalData { get; set; }
    }
}
