using Aurora.API.Backend.Database.Collections;
using Autofac;
using MongoDB.Driver;
using System;
using System.Threading;

namespace Aurora.API.Backend.Database
{
    public class MongoModule : Module
    {
        public string MongoConnectionString { get { return "mongodb://localhost:27017/auroradb"; } }
        public IMongoDatabase Db
        {
            get { return _database.Value; }
        }

        private readonly Lazy<IMongoDatabase> _database;

        public MongoModule(string _connectionString)
        {
            _database = new Lazy<IMongoDatabase>(GetDatabase, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private IMongoDatabase GetDatabase()
        {
            var databaseName = MongoUrl.Create(MongoConnectionString).DatabaseName;
            var client = new MongoClient(MongoConnectionString);
            var database = client.GetDatabase(databaseName, new MongoDatabaseSettings
            {

            });

            return database;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => Db).AsSelf();
            builder.Register((c, p) => Db.GetCollection<DynamicForm>("forms"));
        }
    }
}
