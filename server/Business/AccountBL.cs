using aymk_engine.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aymk_engine.Util.Constants;

namespace aymk_engine.Business
{
    public class AccountBL
    {
        private static List<string> accountColumns;
        private static AccountBL instance;
        private static Repository<Account> repository;
        public static AccountBL GetInstance()
        {
            if (instance == null)
                instance = new AccountBL();

            accountColumns = new List<string>();
            accountColumns.Add("_id");
            accountColumns.Add("username");
            accountColumns.Add("password");
            accountColumns.Add("email"); accountColumns.Add("active");
            accountColumns.Add("createdAt");
            accountColumns.Add("lastLogin");
            accountColumns.Add("tradeSms");
            accountColumns.Add("tradeNotification");
            accountColumns.Add("tradeEmail");
            accountColumns.Add("alertSms");
            accountColumns.Add("alertNotification");
            accountColumns.Add("alertEmail");
            accountColumns.Add("phoneNumber");
            accountColumns.Add("surname");
            accountColumns.Add("name");
            repository = new Repository<Account>();
            return instance;
        }

        public Account GetAccount(ObjectId id)
        {
            return repository.GetItemsById(id);
        }

        public void AddNotification(Notification notification)
        {
            try
            {
                var update = Builders<Account>.Update.Push("notifications", notification);
                repository.UpdateItem(notification.AccountId, update);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Alert> GetAlerts()
        {
            string projects = "{alerts: 1}";
            string match = "{'alerts.active' : true }";

            var pBson =BsonSerializer.Deserialize<BsonDocument>(projects);
            var mBson =BsonSerializer.Deserialize<BsonDocument>(match);

            var aggregate = repository.collection.Aggregate()
                                     .Project(pBson)
                                     .Unwind(i => i["alerts"])
                                     .Match(mBson)
                                     .ToList().Select(j => j["alerts"]);


            var alerts = BsonSerializer.Deserialize<List<Alert>>(aggregate.ToJson());
            return alerts;
        }
    }
}
