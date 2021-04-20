using System.Linq;
using AutoMapper;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Profiles {
    public class TodosProfile : Profile
    {
        public TodosProfile()
        {
            CreateMap<Todo, TodoReadDTO>()
                .ForMember(
                    dto => dto.Labels,
                    expression => expression.MapFrom(todo => todo.Labels.Select(label => label.Id)
                ));

            CreateMap<TodoCreateDTO, Todo>();

            CreateMap<Todo, TodoUpdateDTO>().ReverseMap();
        }
    }
}
