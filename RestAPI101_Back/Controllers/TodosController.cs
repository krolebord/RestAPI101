using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers
{
    [ApiController]
    [Route(APIRoutes.TodosController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class TodosController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IUsersRepository _usersRepository;

        public TodosController(ITodosRepository todosRepository, IUsersRepository usersRepository)
        {
            this._todosRepository = todosRepository;
            this._usersRepository = usersRepository;
        }

        // GET api/todos?mode&labels
        [HttpGet(APIRoutes.Todos.GetAll)]
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

        // GET api/todos/{id}
        [HttpGet(APIRoutes.Todos.GetSpecified, Name = nameof(GetTodoById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoReadDTO> GetTodoById(int id)
        {
            var todo = GetTodo(id);

            if(todo == null)
                return NotFound();

            return Ok(todo.ToReadDTO());
        }

        // POST api/todos/
        [HttpPost(APIRoutes.Todos.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<TodoReadDTO> CreateTodo(TodoCreateDTO todoCreateDto)
        {
            var user = GetUser();
            var todo = todoCreateDto.ToTodo();
            todo.User = user;
            _todosRepository.CreateTodo(todo);

            _todosRepository.SaveChanges();

            var readDto = todo.ToReadDTO();

            // ReSharper disable once RedundantAnonymousTypePropertyName
            return CreatedAtRoute(nameof(GetTodoById), new { Id = readDto.Id }, readDto);
        }

        // PUT api/todos/{id}
        [HttpPut(APIRoutes.Todos.Update)]
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

        // PATCH api/todos/{id}
        [HttpPatch(APIRoutes.Todos.PartialUpdate)]
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

        // DELETE api/todos/{id}
        [HttpDelete(APIRoutes.Todos.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoReadDTO> DeleteTodo(int id)
        {
            var todo = GetTodo(id);

            if (todo == null)
                return NotFound();

            _todosRepository.DeleteTodo(todo);
            _todosRepository.SaveChanges();

            return Ok(todo.ToReadDTO());
        }

        // PUT api/todos/reorder/{id}:{newOrder}
        [HttpPut(APIRoutes.Todos.Reorder)]
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
                ? todos[newOrder].Order : (todos.Count + 1) * _todosRepository.OrderDistance;
            var deltaOrder = nextOrder - prevOrder;

            todo.Order = prevOrder + deltaOrder / 2;

            if(deltaOrder <= 2)
                _todosRepository.NormalizeOrderForUser(user);

            _todosRepository.SaveChanges();

            return Ok();
        }

        // PUT api/todos/label/{id}:{labelId}
        [HttpPut(APIRoutes.Todos.AddLabel)]
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

        // DELETE api/todos/label/{id}:{labelId}
        [HttpDelete(APIRoutes.Todos.RemoveLabel)]
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
