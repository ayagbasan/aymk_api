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
    public class Account
    {
        public static string collectionName = "accounts";
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("lastLogin")]
        public DateTime? LastLogin { get; set; }

        [BsonElement("tradeSms")]
        public bool TradeSms { get; set; }

        [BsonElement("tradeNotification")]
        public bool TradeNotification { get; set; }

        [BsonElement("tradeEmail")]
        public bool TradeEmail { get; set; }

        [BsonElement("alertSms")]
        public bool AlertSms { get; set; }

        [BsonElement("alertNotification")]
        public bool AlertNotification { get; set; }

        [BsonElement("alertEmail")]
        public bool AlertEmail { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }


        [BsonElement("exchanges")]
        public List<Exchange> Exchanges { get; set; }

        [BsonElement("alerts")]
        public List<Alert> Alerts { get; set; }

        [BsonElement("notifications")]
        public List<Notification> Notifications { get; set; }

    }
}
