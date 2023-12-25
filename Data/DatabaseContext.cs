using EventFinder.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace EventFinder.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Event> Events
        {
            get { return _database.GetCollection<Event>("Events"); }
        }

        public IMongoCollection<EventType> EventTypes
        {
            get { return _database.GetCollection<EventType>("EventTypes"); }
        }

        public IMongoCollection<User> Users
        {
            get { return _database.GetCollection<User>("Users"); }
        }
    }
}
