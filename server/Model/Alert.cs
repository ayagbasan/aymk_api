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
    public class Alert
    {       
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("marketId")]
        public ObjectId MarketId { get; set; }
        public CatalogMarket Market { get; set; }

        [BsonElement("accountId")]
        public ObjectId AccountId { get; set; }

        [BsonElement("belowValue")]
        public decimal BelowValue { get; set; }

        [BsonElement("savedValue")]
        public decimal SavedValue { get; set; }

        [BsonElement("aboveValue")]
        public decimal AboveValue { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updateAt")]
        public DateTime? UpdateAt { get; set; }

        [BsonElement("isPersistent")]
        public bool IsPersistent { get; set; }    

        [BsonElement("alertComment")]
        public string AlertComment { get; set; }

        [BsonElement("valueType")]
        public string ValueType { get; set; } 

    }
}
