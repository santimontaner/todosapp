using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodosApp.ViewModels.Task;
using TodosApp.ViewModels;
using TodosApp.Repositories.Interfaces;
using AutoMapper;

namespace TodosApp.Controllers;

public class TaskController : Controller
{
    private readonly ILogger<TaskController> _logger;
    private readonly IMapper _mapper;
    private readonly ITasksRepository _tasksRepository;
    
    public TaskController(IMapper mapper, ILogger<TaskController> logger, ITasksRepository tasksRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tasksRepository = tasksRepository;
    }

    public async Task<IActionResult> Index()
    {
        var tasksListViewModel = (await _tasksRepository.ListTasksAsync()).Select(t => _mapper.Map<TaskViewModel>(t));        
        return View(tasksListViewModel);
    }

    [Route("Task/Task/{taskId}")]
    [HttpGet]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        _logger.LogInformation($"Getting task {taskId}");
        var taskViewModel = _mapper.Map<TaskViewModel>(await _tasksRepository.GetTaskById(taskId));        
        return PartialView("_PartialEntryRow", taskViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveNew(TaskViewModel model)
    {
        _logger.LogInformation($"SavingNew {model.Description}");
        await _tasksRepository.CreateTaskAsync(model.Description, model.DueDate);
        return RedirectToAction("Index");
    }

    [Route("Task/Delete/{taskId}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid taskId)
    {
        _logger.LogInformation($"Deleting {taskId}");
        await _tasksRepository.DeleteTaskAsync(taskId);
        return new EmptyResult();
    }

    [Route("Task/Edit")]
    [HttpPut]
    public async Task<IActionResult> UpdateTask(TaskViewModel task)
    {
        _logger.LogInformation($"Updating task {task.Id}");
        _logger.LogInformation($"Updating task {task.Description}");
        _logger.LogInformation($"Updating task {task.DueDate}");
        await _tasksRepository.UpdateTaskDescriptionAsync(task.Id, task.Description, task.DueDate);
        return PartialView("_PartialEntryRow", task);
    }

    [Route("Task/Edit/{taskId}")]
    [HttpGet]
    public async Task<IActionResult> EditTask(Guid taskId)
    {
        _logger.LogInformation($"Editing task {taskId}");
        var taskViewModel = _mapper.Map<TaskViewModel>(await _tasksRepository.GetTaskById(taskId));        
        return PartialView("_PartialEditableEntryRow", taskViewModel);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
