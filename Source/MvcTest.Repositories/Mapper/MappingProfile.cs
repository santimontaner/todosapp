using AutoMapper;
using MvcTest.DomainModel.Tasks;

namespace MvcTest.Repositories.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<TaskModel, TaskEntity>()
        .ReverseMap()
        .ConstructUsing(x => new TaskModel(x.Id)
        {
            Description = x.Description,
            DueDate = x.DueDate
        });
	}
}

