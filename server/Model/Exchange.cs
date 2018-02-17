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
    public class Exchange
    {       
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("secretKey")]
        public string SecretKey { get; set; }

        [BsonElement("apiKey")]
        public string ApiKey { get; set; }

        [BsonElement("caption")]
        public string Caption { get; set; }

        [BsonElement("exchangeId")]
        public ObjectId ExchangeId { get; set; }

        [BsonElement("accountId")]
        public ObjectId AccountId { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updateAt")]
        public DateTime? UpdateAt { get; set; }
        

        [BsonElement("tradeEnabled")]
        public bool TradeEnabled { get; set; } 

    }
}
