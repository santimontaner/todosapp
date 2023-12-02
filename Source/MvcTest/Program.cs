using Mongo.Migration.Documents;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Driver;
using MvcTest.Controllers;
using MvcTest.Repositories;
using MvcTest.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb:ConnectionString");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new CounterService());
builder.Services.AddSingleton<IMongoClient, MongoClient>(
    sp => new MongoClient(mongoConnectionString));
builder.Services.AddSingleton<ITasksRepository, TasksRepository>();

builder.Services.AddAutoMapper(typeof(TasksRepository), typeof(TaskController));

builder.Services.AddMigration(new MongoMigrationSettings()
                         {
                             ConnectionString = mongoConnectionString,
                             Database = "tasksDb",
                             DatabaseMigrationVersion = new DocumentVersion("3.0.0")
                         });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
