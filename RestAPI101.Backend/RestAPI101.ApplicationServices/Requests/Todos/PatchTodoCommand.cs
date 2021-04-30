using Microsoft.AspNetCore.JsonPatch;
using OneOf;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class PatchTodoCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int Id { get; }

        public JsonPatchDocument<TodoUpdateDTO> Patch { get; }

        public PatchTodoCommand(string? userLogin, int id, JsonPatchDocument<TodoUpdateDTO> patch) : base(userLogin)
        {
            Id = id;
            Patch = patch;
        }
    }
}
