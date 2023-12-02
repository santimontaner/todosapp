namespace MvcTest.DomainModel.Tasks;

public class TaskModel
{

    public TaskModel(Guid id)
    {
        Id = id;
    }

    public Guid Id {get; init;}

    public string Description {get; set;} = string.Empty;

    public DateTime DueDate {get; set;}        
}
