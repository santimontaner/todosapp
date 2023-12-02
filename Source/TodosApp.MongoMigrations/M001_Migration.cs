using Mongo.Migration.Migrations.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TodosApp.MongoMigrations;
public class M001_Migration : DatabaseMigration
{
    public M001_Migration() : base("1.0.0")
    {
    }

    public override async void Down(IMongoDatabase db)
    {
        var tasksCollection = db.GetCollection<BsonDocument>("tasks");
        var filterDefinitionBuilder = new FilterDefinitionBuilder<BsonDocument>();
        var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();

        var updateDefinition = updateDefinitionBuilder.Unset("Status");
                
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition);       
    }

    public override async void Up(IMongoDatabase db)
    {
        var tasksCollection = db.GetCollection<BsonDocument>("tasks");
        var filterDefinitionBuilder = new FilterDefinitionBuilder<BsonDocument>();
        var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();

        var subdoc = new BsonDocument();
        subdoc["CoolNewProperty"] = "333";
                
        var updateDefinition = updateDefinitionBuilder.Set("Status", subdoc);
                
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition);       
    }
}
