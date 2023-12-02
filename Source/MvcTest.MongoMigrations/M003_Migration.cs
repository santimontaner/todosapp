using System.Reflection.Metadata;
using Mongo.Migration.Migrations.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MvcTest.MongoMigrations;
public class M003_Migration : DatabaseMigration
{
    public M003_Migration() : base("3.0.0")
    {
    }

    public override async void Down(IMongoDatabase db)
    {
        var tasksCollection = db.GetCollection<BsonDocument>("tasks");
        var filterDefinitionBuilder = new FilterDefinitionBuilder<BsonDocument>();
        var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();
        
        var updateDefinition = updateDefinitionBuilder.Set("Status", "333");
        var updateDefinition_1 = updateDefinitionBuilder.Unset("Family");
                
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition);       
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition_1);       
    }

    public override async void Up(IMongoDatabase db)
    {
        var tasksCollection = db.GetCollection<BsonDocument>("tasks");
        var filterDefinitionBuilder = new FilterDefinitionBuilder<BsonDocument>();
        var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();
                       
        var updateDefinition = updateDefinitionBuilder.Unset("Status");
        var updateDefinition1 = updateDefinitionBuilder.Set("Family", true);
                
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition);       
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition1);       
    }
}
