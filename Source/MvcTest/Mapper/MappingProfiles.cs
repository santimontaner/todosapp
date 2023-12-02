using AutoMapper;
using MvcTest.DomainModel.Tasks;
using MvcTest.ViewModels.Task;

namespace MvcTest;

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