using MvcTest.DomainModel.Tasks;

namespace MvcTest.Repositories.Interfaces
{
    public interface ITasksRepository
    {
        public Task CreateTaskAsync(string description, DateTime dueDate);    

        public Task<IEnumerable<TaskModel>> ListTasksAsync();
        
        public Task UpdateTaskDescriptionAsync(Guid id, string description, DateTime dueDate);

        public Task DeleteTaskAsync(Guid id);   

        public Task<TaskModel> GetTaskById(Guid id);   
    }    
}