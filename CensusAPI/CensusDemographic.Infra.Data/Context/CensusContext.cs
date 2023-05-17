using MongoDB.Driver;
using CensusDemographic.Domain.Models;

namespace CensusDemographic.Infra.Data.Context
{
    public class CensusContext
    {
        public CensusContext(IMongoDatabase database) => Database = database;
        public IMongoDatabase Database { get; private set; }
        public IMongoCollection<Person> Persons => Database.GetCollection<Person>("Persons");
        public IMongoCollection<User> Users => Database.GetCollection<User>("Users");
    }
}
