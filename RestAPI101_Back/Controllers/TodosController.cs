using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController] 
    [Route("api/todos")]
    [Authorize]
    public class TodosController : ControllerBase {
        private readonly ITodosRepository todosRepository;
        private IMapper mapper;
        
        public TodosController(ITodosRepository todosRepository, IMapper mapper) {
            this.todosRepository = todosRepository;
            this.mapper = mapper;
        }
        
        // GET api/todos/
        [HttpGet] 
        public ActionResult<IEnumerable<TodoReadDTO>> GetAllTodos() {
            var todos = todosRepository.GetAllTodos();

            if (todos != null && todos.Any())
                return Ok(mapper.Map<IEnumerable<TodoReadDTO>>(todos));
            return NoContent();
        }

        // GET api/todos/{id}
        [HttpGet("{id}", Name = nameof(GetTodoById))] 
        public ActionResult<TodoReadDTO> GetTodoById(long id) {
            var item = todosRepository.GetTodoById(id);
            if(item != null)
                return Ok(mapper.Map<Todo, TodoReadDTO>(item));
            return NotFound();
        }
        
        // POST api/todos/
        [HttpPost]
        public ActionResult<TodoReadDTO> CreateTodo(TodoCreateDTO todoCreateDto) {
            var todo = mapper.Map<TodoCreateDTO, Todo>(todoCreateDto);
            todosRepository.CreateTodo(todo);
            
            if (!todosRepository.SaveChanges())
                return StatusCode(StatusCodes.Status500InternalServerError);

            var commandReadDto = mapper.Map<Todo, TodoReadDTO>(todo);
            
            return CreatedAtRoute(nameof(GetTodoById), new { Id = commandReadDto.Id }, commandReadDto);
        }
        
        // PUT api/todos/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateTodo(long id, TodoUpdateDTO todoUpdateDto) {
            var todo = todosRepository.GetTodoById(id);

            if (todo == null)
                return NotFound();
            
            mapper.Map<TodoUpdateDTO, Todo>(todoUpdateDto, todo);
            
            todosRepository.SaveChanges();

            return NoContent();
        }
        
        // PATCH api/todos/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateTodo(long id, JsonPatchDocument<TodoUpdateDTO> patch) {
            var todo = todosRepository.GetTodoById(id);

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

        // DELETE api/todos/{id}
        [HttpDelete("{id}")]
        public ActionResult<TodoReadDTO> DeleteTodo(long id) {
            var todo = todosRepository.GetTodoById(id);

            if (todo == null)
                return NotFound();
            
            todosRepository.DeleteTodo(todo);
            todosRepository.SaveChanges();

            return Ok(todo);
        }
    }
}