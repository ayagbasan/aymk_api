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
    public class Notification
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("accountId")]
        public ObjectId AccountId { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("style")]
        public string Style { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("status")]
        public string  Status { get; set; }

        [BsonElement("statusMessage")]
        public string StatusMessage { get; set; }

        [BsonElement("completedAt")]
        public DateTime CompletedAt { get; set; }

        [BsonElement("repeat")]
        public int Repeat { get; set; }


    }
}
