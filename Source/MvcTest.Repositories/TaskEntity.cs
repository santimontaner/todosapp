using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MvcTest.Repositories
{
    [BsonIgnoreExtraElements]
    internal class TaskEntity
    {        
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id {get; set;}

        public string Description {get; set;} = string.Empty;

        public DateTime DueDate {get; set;}        
    }
}