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
    public class CatalogExchange
    {
        public static string collectionName = "catalogexchanges";
        public static ObjectId EXC_BITTREX = new ObjectId("5a5a76f667d6713bb4a10d69");
        public static ObjectId EXC_POLONIEX = new ObjectId("5a5a76ec67d6713bb4a10d68");
        public static ObjectId EXC_BINANCE = new ObjectId("5a5a772c67d6713bb4a10d6a");
        public static ObjectId EXC_BITFINEX = new ObjectId("5a5a773367d6713bb4a10d6b");
        public static ObjectId EXC_BITHUMB = new ObjectId("5a5a774167d6713bb4a10d6c");
        public static ObjectId EXC_GDAX = new ObjectId("5a5a774d67d6713bb4a10d6d");
        public static ObjectId EXC_KRAKEN = new ObjectId("5a5a775867d6713bb4a10d6e"); 

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("exchangeName")]
        public string ExchangeName { get; set; }

        [BsonElement("marketSplitterChar")]
        public string Splitter { get; set; }

        [BsonElement("webSiteUrl")]
        public string WebSiteUrl { get; set; }        

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }         

    }
}
