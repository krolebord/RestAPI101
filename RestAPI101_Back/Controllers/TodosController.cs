using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<TodoReadDTO>> GetAllTodos() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);

            var todos = user.Todos;

            if (todos == null || !todos.Any())
                return NoContent();
            
            
            var mappedTodos = mapper.Map<IEnumerable<TodoReadDTO>>(todos.OrderBy(todo => todo.Order));
            int order = 0;
            //foreach (var todo in mappedTodos)
            //    todo.Order = order++;
            
            return Ok(mappedTodos);
        }

        // GET api/todos/{id}
        [HttpGet(APIRoutes.Todos.GetSpecified, Name = nameof(GetTodoById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoReadDTO> GetTodoById(int id) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var todo = user.Todos.FirstOrDefault(x => x.Id == id);

            if(todo == null)
                return NotFound();
            
            return Ok(mapper.Map<Todo, TodoReadDTO>(todo));
        }

        // POST api/todos/
        [HttpPost(APIRoutes.Todos.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<TodoReadDTO> CreateTodo(TodoCreateDTO todoCreateDto) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var todo = mapper.Map<TodoCreateDTO, Todo>(todoCreateDto);
            todo.User = user;
            todosRepository.CreateTodo(todo);

            todosRepository.SaveChanges();

            var commandReadDto = mapper.Map<Todo, TodoReadDTO>(todo);
            
            return CreatedAtRoute(nameof(GetTodoById), new { Id = commandReadDto.Id }, commandReadDto);
        }
        
        // PUT api/todos/{id}
        [HttpPut(APIRoutes.Todos.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateTodo(int id, TodoUpdateDTO todoUpdateDto) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var todo = user.Todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
                return NotFound();
            
            mapper.Map<TodoUpdateDTO, Todo>(todoUpdateDto, todo);
            
            todosRepository.SaveChanges();

            return NoContent();
        }
        
        // PATCH api/todos/{id}
        [HttpPatch(APIRoutes.Todos.PartialUpdate)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PartialUpdateTodo(int id, JsonPatchDocument<TodoUpdateDTO> patch) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var todo = user.Todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
                return NotFound();

            var todoToPatch = mapper.Map<Todo, TodoUpdateDTO>(todo);
            patch.ApplyTo(todoToPatch, ModelState);
            if (!TryValidateModel(todoToPatch))
                return ValidationProblem(ModelState);

            mapper.Map<TodoUpdateDTO, Todo>(todoToPatch, todo);
            todosRepository.SaveChanges();

            return NoContent();
        }

        // POST api/todos/reorder
        [HttpPost(APIRoutes.Todos.Reorder)]
        public ActionResult ReorderTodos(int id, int newOrder) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var todos = user.Todos?.OrderBy(x => x.Order).ToList();

            if (todos == null || !todos.Any()) return NotFound();
            
            var todo = todos.FirstOrDefault(x => x.Id == id);
            if (todo == null) return NotFound();
            
            int prevOrder = newOrder > 0 ? 
                todos[newOrder-1].Order : 0;
            int nextOrder = newOrder < todos.Count
                ? todos[newOrder].Order : (todos.Count + 1) * todosRepository.OrderDistance;
            int deltaOrder = nextOrder - prevOrder;
            
            todo.Order = prevOrder + deltaOrder / 2;
            
            if(deltaOrder <= 2)
                todosRepository.NormalizeOrderForUser(user);
            
            todosRepository.SaveChanges();

            return Ok();
        }
        
        // DELETE api/todos/{id}
        [HttpDelete(APIRoutes.Todos.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoReadDTO> DeleteTodo(int id) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var todo = user.Todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
                return NotFound();
            
            todosRepository.DeleteTodo(todo);
            todosRepository.SaveChanges();

            return Ok(mapper.Map<TodoReadDTO>(todo));
        }
    }
}