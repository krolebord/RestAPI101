using AutoMapper;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Profiles {
    public class TodosProfile : Profile {
        public TodosProfile() {
            CreateMap<Todo, TodoReadDTO>();

            CreateMap<TodoCreateDTO, Todo>();
            
            CreateMap<Todo, TodoUpdateDTO>();
        }
    }
}