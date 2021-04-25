using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.Data.RepositoryExtensions;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;
using RestAPI101.WebAPI.Filters;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.TodosController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class TodosController : ControllerBase
    {
        private readonly IRepository<Todo> _todosRepository;
        private readonly IRepository<User> _usersRepository;

        public TodosController(IRepository<Todo> todosRepository, IRepository<User> usersRepository)
        {
            this._todosRepository = todosRepository;
            this._usersRepository = usersRepository;
        }

        public enum TodoFilterMode { Or, And }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<TodoReadDTO>> GetTodos([FromQuery]int[]? labels = null, [FromQuery]TodoFilterMode mode = TodoFilterMode.Or)
        {
            if (labels != null && labels.Any())
                return GetLabeledTodos(labels.ToHashSet(), mode);
            return GetAllTodos();
        }

        private ActionResult<IEnumerable<TodoReadDTO>> GetAllTodos()
        {
            var user = GetUser();

            var todos = user.Todos;

            if (!todos.Any())
                return NoContent();

            var mappedTodos = todos
                .OrderBy(todo => todo.Order)
                .Select(todo => todo.ToReadDTO());
            return Ok(mappedTodos);
        }

        private ActionResult<IEnumerable<TodoReadDTO>> GetLabeledTodos(IReadOnlySet<int> labelIds, TodoFilterMode mode)
        {
            var user = GetUser();

            Func<Todo, bool> predicate = mode switch {
                TodoFilterMode.And =>
                    todo => labelIds.All(labelId => todo.Labels.Any(label => label.Id == labelId)),
                _ =>
                    todo => todo.Labels.Any(label => labelIds.Contains(label.Id))
            };
            var todos = user.Todos
                .Where(predicate).ToList();

            if (!todos.Any())
                return NoContent();

            var mappedTodos = todos
                .OrderBy(todo => todo.Order)
                .Select(todo => todo.ToReadDTO());
            return Ok(mappedTodos);
        }

        [HttpGet("{id:int}", Name = nameof(GetTodoById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoReadDTO> GetTodoById(int id)
        {
            var todo = GetTodo(id);

            if(todo == null)
                return NotFound();

            return Ok(todo.ToReadDTO());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<TodoReadDTO> CreateTodo(TodoCreateDTO todoCreateDto)
        {
            var user = GetUser();

            var todo = todoCreateDto.ToTodo();
            todo.User = user;

            _todosRepository.Add(todo);

            _todosRepository.SaveChanges();

            var readDto = todo.ToReadDTO();

            // ReSharper disable once RedundantAnonymousTypePropertyName
            return CreatedAtRoute(nameof(GetTodoById), new { Id = readDto.Id }, readDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateTodo(int id, TodoUpdateDTO todoUpdateDto)
        {
            var todo = GetTodo(id);

            if (todo == null)
                return NotFound();

            todo.MapUpdateDTO(todoUpdateDto);

            _todosRepository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PartialUpdateTodo(int id, JsonPatchDocument<TodoUpdateDTO> patch)
        {
            var todo = GetTodo(id);

            if (todo == null)
                return NotFound();

            var todoToPatch = todo.ToUpdateDTO();
            patch.ApplyTo(todoToPatch, ModelState);
            if (!TryValidateModel(todoToPatch))
                return ValidationProblem(ModelState);

            todo.MapUpdateDTO(todoToPatch);
            _todosRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoReadDTO> DeleteTodo(int id)
        {
            var todo = GetTodo(id);

            if (todo == null)
                return NotFound();

            _todosRepository.Delete(todo);
            _todosRepository.SaveChanges();

            return Ok(todo.ToReadDTO());
        }

        [HttpPut("reorder/{id:int}:{newOrder:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult ReorderTodos(int id, int newOrder)
        {
            var user = GetUser();
            var todos = user.Todos.OrderBy(x => x.Order).ToList();

            if (!todos.Any()) return NotFound();

            var todo = todos.FirstOrDefault(x => x.Id == id);
            if (todo == null) return NotFound();

            var prevOrder = newOrder > 0 ?
                todos[newOrder-1].Order : 0;
            var nextOrder = newOrder < todos.Count
                ? todos[newOrder].Order : (todos.Count + 1) * 1024;
            var deltaOrder = nextOrder - prevOrder;

            todo.Order = prevOrder + deltaOrder / 2;

            if (deltaOrder <= 2)
                for (var i = 0; i < todos.Count; i++)
                    todos[i].Order = (i + 1) * 1024;

            _todosRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("label/{id:int}:{labelId:int}")]
        public ActionResult AddLabel(int id, int labelId)
        {
            var todo = GetTodo(id);
            var label = GetLabel(labelId);

            if (todo == null || label == null)
                return NotFound();

            if (todo.Labels.Contains(label)) return Ok();

            todo.Labels.Add(label);

            _todosRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete("label/{id:int}:{labelId:int}")]
        public ActionResult RemoveLabel(int id, int labelId)
        {
            var todo = GetTodo(id);
            var label = GetLabel(labelId);

            if (todo == null || label == null || !todo.Labels.Contains(label))
                return NotFound();

            todo.Labels.Remove(label);

            _todosRepository.SaveChanges();

            return Ok();
        }

        private User GetUser() =>
            _usersRepository.GetUserByLogin(User.Identity!.Name!)!;

        private Todo? GetTodo(int id) =>
            GetUser().Todos.FirstOrDefault(todo => todo.Id == id);

        private Label? GetLabel(int id) =>
            GetUser().Labels.FirstOrDefault(label => label.Id == id);
    }
}
