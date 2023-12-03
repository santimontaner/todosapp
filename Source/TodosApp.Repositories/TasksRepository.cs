using AutoMapper;
using MongoDB.Driver;
using TodosApp.DomainModel.Tasks;
using TodosApp.Repositories.Interfaces;

namespace TodosApp.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDb;
    private readonly IMongoCollection<TaskEntity> _tasksCollection;
    private readonly IMapper _mapper;

    private static FilterDefinitionBuilder<TaskEntity> TaskFilterBuilder = new();
    private static UpdateDefinitionBuilder<TaskEntity> TaskUpdateBuilder = new();
    
    public TasksRepository(IMongoClient mongoClient, IMapper mapper)
    {
        _mapper = mapper;
        _mongoClient = mongoClient;
        _mongoDb = _mongoClient.GetDatabase("tasksDb");
        _tasksCollection = _mongoDb.GetCollection<TaskEntity>("tasks");
    }

    public Task CreateTaskAsync(Guid id, string description, DateTime dueDate)
    {
        var newTask = new TaskEntity()
        {
            Id = id,
            Description = description,
            DueDate = dueDate
        };
        return _tasksCollection.InsertOneAsync(newTask);
    }

    public Task UpdateTaskDescriptionAsync(Guid id, string description, DateTime dueDate)
    {            
        var updateDefinition = TaskUpdateBuilder
            .Set(x => x.Description, description)
            .Set(x => x.DueDate, dueDate);
        return _tasksCollection.FindOneAndUpdateAsync(TaskFilterBuilder.Eq(x => x.Id, id), updateDefinition);
    }

    public async Task<IEnumerable<TaskModel>> ListTasksAsync()
    {
        return (await _tasksCollection.FindAsync(_ => true)).ToList().Select(t => _mapper.Map<TaskModel>(t));
    }

    public Task DeleteTaskAsync(Guid id)
    {
        return _tasksCollection.DeleteOneAsync(TaskFilterBuilder.Eq(x => x.Id, id));
    }

    public async Task<TaskModel> GetTaskById(Guid id)
    {
        return _mapper.Map<TaskModel>((await _tasksCollection.FindAsync(TaskFilterBuilder.Eq(x => x.Id, id))).First());
    }
}
