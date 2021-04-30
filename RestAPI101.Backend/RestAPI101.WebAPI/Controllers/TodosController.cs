using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.Enums;
using RestAPI101.WebAPI.Filters;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.TodosController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region Queries

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<TodoReadDTO>>> GetTodos([FromQuery]int[]? labels = null, [FromQuery]TodoFilterMode mode = TodoFilterMode.Or)
        {
            var request = new GetAllTodosQuery(User.Identity?.Name, labels?.ToHashSet() ?? new HashSet<int>(), mode);
            var todos = await _mediator.Send(request);

            return todos.Any() ? Ok(todos) : NoContent();
        }

        [HttpGet("{id:int}", Name = nameof(GetTodoById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoReadDTO>> GetTodoById(int id)
        {
            var request = new GetSpecifiedTodoQuery(User.Identity?.Name, id);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult<TodoReadDTO>>(
                todoReadDto => Ok(todoReadDto),
                notFound => NotFound()
            );
        }

        #endregion

        #region Commands

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoReadDTO>> CreateTodo(TodoCreateDTO todoCreateDto)
        {
            var request = new CreateTodoCommand(User.Identity?.Name, todoCreateDto);
            var createdTodo = await _mediator.Send(request);

            return CreatedAtRoute(nameof(GetTodoById), new { Id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTodo(int id, TodoUpdateDTO todoUpdateDto)
        {
            var request = new UpdateTodoCommand(User.Identity?.Name, id, todoUpdateDto);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => NoContent(),
                notFound => NotFound()
            );
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartialUpdateTodo(int id, JsonPatchDocument<TodoUpdateDTO> patch)
        {
            var request = new PatchTodoCommand(User.Identity?.Name, id, patch);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => NoContent(),
                notFound => NotFound()
            );
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var request = new DeleteTodoCommand(User.Identity?.Name, id);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => NoContent(),
                notFound => NotFound()
            );
        }

        [HttpPut("reorder/{id:int}:{newOrder:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ReorderTodos(int id, int newOrder)
        {
            var request = new ReorderTodoCommand(User.Identity?.Name, id, newOrder);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => Ok(),
                notFound => NotFound()
            );
        }

        [HttpPut("label/{id:int}:{labelId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddLabel(int id, int labelId)
        {
            var request = new AddLabelTodoCommand(User.Identity?.Name, id, labelId);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => Ok(),
                notFound => NotFound()
            );
        }

        [HttpDelete("label/{id:int}:{labelId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveLabel(int id, int labelId)
        {
            var request = new AddLabelTodoCommand(User.Identity?.Name, id, labelId);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => Ok(),
                notFound => NotFound()
            );
        }

        #endregion
    }
}
