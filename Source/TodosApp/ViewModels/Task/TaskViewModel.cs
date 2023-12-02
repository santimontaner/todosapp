namespace TodosApp.ViewModels.Task;

public class TaskViewModel
{
    public Guid Id {get;set;}

    public string Description {get; set;} = string.Empty;

    public DateTime DueDate {get; set;}

    public bool Editable {get; set;}  = false;  
}