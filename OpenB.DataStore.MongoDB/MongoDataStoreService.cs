using MongoDB.Driver;
using OpenB.Core;
using OpenB.Core.Data;

namespace OpenB.DataStore.MongoDB
{
    public class MongoDataStoreService : IDataStoreService
    {
        private readonly MongoDatabase _database;

        public MongoDataStoreService(string servername, string repository)
        {
           MongoClient mongoClient = new MongoClient(string.Format("mongodb://{0}", servername));
           MongoServer server =  mongoClient.GetServer();
           _database = server.GetDatabase(repository);
        }

        public T GetModel<T>(string key) where T : IModel
        {
            return _database.GetCollection(typeof(T).Name).FindOneAs<T>();
        }

        public void CreateModel<T>(T model) where T : IModel
        {
            var collection = _database.GetCollection<T>(model.GetType().Name);

            collection.Insert(model);
        }
    }
}