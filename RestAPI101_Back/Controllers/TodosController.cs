using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController] 
    [Route(APIRoutes.TodosController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class TodosController : ControllerBase {
        private readonly ITodosRepository todosRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;
        
        public TodosController(ITodosRepository todosRepository, IUsersRepository usersRepository, IMapper mapper) {
            this.todosRepository = todosRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }
        
        // GET api/todos/
        [HttpGet(APIRoutes.Todos.GetAll)]
        public ActionResult<IEnumerable<TodoReadDTO>> GetAllTodos() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);

            var todos = user.Todos?.ToList();

            if (todos != null && todos.Any())
                return Ok(mapper.Map<IEnumerable<TodoReadDTO>>(todos));
            return NoContent();
        }

        // GET api/todos/{id}
        [HttpGet(APIRoutes.Todos.GetSpecified, Name = nameof(GetTodoById))] 
        public ActionResult<TodoReadDTO> GetTodoById(int id) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var item = user.Todos.FirstOrDefault(todo => todo.Id == id);

            if(item == null)
                return NotFound();
            
            return Ok(mapper.Map<Todo, TodoReadDTO>(item));
        }

        // POST api/todos/
        [HttpPost(APIRoutes.Todos.Create)]
        public ActionResult<TodoReadDTO> CreateTodo(TodoCreateDTO todoCreateDto) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var item = mapper.Map<TodoCreateDTO, Todo>(todoCreateDto);
            item.User = user;
            todosRepository.CreateTodo(item);

            todosRepository.SaveChanges();

            var commandReadDto = mapper.Map<Todo, TodoReadDTO>(item);
            
            return CreatedAtRoute(nameof(GetTodoById), new { Id = commandReadDto.Id }, commandReadDto);
        }
        
        // PUT api/todos/{id}
        [HttpPut(APIRoutes.Todos.Update)]
        public ActionResult UpdateTodo(int id, TodoUpdateDTO todoUpdateDto) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var item = user.Todos.FirstOrDefault(todo => todo.Id == id);

            if (item == null)
                return NotFound();
            
            mapper.Map<TodoUpdateDTO, Todo>(todoUpdateDto, item);
            
            todosRepository.SaveChanges();

            return NoContent();
        }
        
        // PATCH api/todos/{id}
        [HttpPatch(APIRoutes.Todos.PartialUpdate)]
        public ActionResult PartialUpdateTodo(int id, JsonPatchDocument<TodoUpdateDTO> patch) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var item = user.Todos.FirstOrDefault(todo => todo.Id == id);

            if (item == null)
                return NotFound();

            var todoToPatch = mapper.Map<Todo, TodoUpdateDTO>(item);
            patch.ApplyTo(todoToPatch, ModelState);
            if (!TryValidateModel(todoToPatch))
                return ValidationProblem(ModelState);

            mapper.Map<TodoUpdateDTO, Todo>(todoToPatch, item);
            todosRepository.SaveChanges();

            return NoContent();
        }

        // DELETE api/todos/{id}
        [HttpDelete(APIRoutes.Todos.Delete)]
        public ActionResult<TodoReadDTO> DeleteTodo(int id) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var item = user.Todos.FirstOrDefault(todo => todo.Id == id);

            if (item == null)
                return NotFound();
            
            todosRepository.DeleteTodo(item);
            todosRepository.SaveChanges();

            return Ok(mapper.Map<TodoReadDTO>(item));
        }
    }
}