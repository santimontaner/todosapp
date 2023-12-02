﻿using Mongo.Migration.Migrations.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MvcTest.MongoMigrations;
public class M002_Migration : DatabaseMigration
{
    public M002_Migration() : base("2.0.0")
    {
    }

    public override async void Down(IMongoDatabase db)
    {
        var tasksCollection = db.GetCollection<BsonDocument>("tasks");
        var filterDefinitionBuilder = new FilterDefinitionBuilder<BsonDocument>();
        var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();

        var subdoc = new BsonDocument();
        subdoc["CoolNewProperty"] = "333";

        var updateDefinition = updateDefinitionBuilder.Set("Status", subdoc);
                
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition);       
    }

    public override async void Up(IMongoDatabase db)
    {
        var tasksCollection = db.GetCollection<BsonDocument>("tasks");
        var filterDefinitionBuilder = new FilterDefinitionBuilder<BsonDocument>();
        var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();
                       
        var updateDefinition = updateDefinitionBuilder.Set("Status", "333");
                
        await tasksCollection.UpdateManyAsync(filterDefinitionBuilder.Empty, updateDefinition);       
    }
}
