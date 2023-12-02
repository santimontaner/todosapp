using AutoMapper;
using TodosApp.DomainModel.Tasks;
using TodosApp.ViewModels.Task;

namespace TodosApp;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<TaskModel, TaskViewModel>()
        .ReverseMap()
        .ConstructUsing(x => new TaskModel(x.Id)
        {
            Description = x.Description,
            DueDate = x.DueDate
        });
	}
}