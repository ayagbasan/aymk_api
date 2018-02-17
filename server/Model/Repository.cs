using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace aymk_engine.Model
{
    public class Repository<T>
    {
        public IMongoClient client;
        public IMongoDatabase database;
        public IMongoCollection<T> collection;

        public Repository()
        {
            string collectionName = string.Empty;

            Type collecyionType = typeof(T);
            if (collecyionType == typeof(Account))
                collectionName = Account.collectionName;
            else if (collecyionType == typeof(CatalogExchange))
                collectionName = CatalogExchange.collectionName;
            else if (collecyionType == typeof(CatalogMarket))
                collectionName = CatalogMarket.collectionName;

            if (!string.IsNullOrEmpty(collectionName))
            {
                client = new MongoClient(Form1.connectionString);
                database = client.GetDatabase(Form1.databaseName);
                collection = database.GetCollection<T>(collectionName);
            }


        }

        public void Insert(T item)
        {
            collection.InsertOne(item);
        }

        public List<T> GetAllList()
        {
            return collection.Find(new BsonDocument()).ToList();
        }       

        
        public List<T> GetItemsByField(string fieldName, string fieldValue)
        {
            var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
            var result = collection.Find(filter).ToList();

            return result;
        }

        public T GetItemsById(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id.ToString());
            var result = collection.Find(filter).SingleOrDefault();

            return result;
        }
         
        public List<T> GetItems(int startingFrom, int count)
        {
            var result = collection.Find(new BsonDocument())
                                               .Skip(startingFrom)
                                               .Limit(count)
                                               .ToList();

            return result;
        }

        public bool UpdateItem(ObjectId id, string updateFieldName, string updateFieldValue)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var update = Builders<T>.Update.Set(updateFieldName, updateFieldValue).Set("updatedAt", DateTime.Now);

            var result = collection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        public bool UpdateItem(ObjectId id, UpdateDefinition<T> update)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);

            var result = collection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        public bool DeleteItemById(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = collection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }

        public long DeleteAllItems()
        {
            var filter = new BsonDocument();
            var result = collection.DeleteMany(filter);
            return result.DeletedCount;
        }
         
 
        
    }
}
