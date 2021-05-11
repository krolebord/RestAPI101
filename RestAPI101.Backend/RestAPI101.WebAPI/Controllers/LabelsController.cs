using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.ApplicationServices.DTOs.Label;
using RestAPI101.ApplicationServices.Requests.Labels;
using RestAPI101.WebAPI.Filters;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.LabelsController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class LabelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LabelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<LabelReadDTO>>> GetAllLabels()
        {
            var request = new GetAllLabelsQuery(User.Identity?.Name);
            var response = await _mediator.Send(request);

            return response.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id:int}", Name = nameof(GetLabelById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LabelReadDTO>> GetLabelById([FromRoute]int id)
        {
            var request = new GetSpecifiedLabelQuery(User.Identity?.Name, id);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult<LabelReadDTO>>(
                labelDto => Ok(labelDto),
                notFound => NotFound()
            );
        }

        #endregion

        #region Commands

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateLabel([FromBody]LabelCreateDTO labelDto)
        {
            var request = new CreateLabelCommand(User.Identity?.Name, labelDto);
            var readDto = await _mediator.Send(request);

            return CreatedAtRoute(nameof(GetLabelById), new { Id = readDto.Id }, readDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateLabel([FromRoute]int id, [FromBody]LabelUpdateDTO labelDto)
        {
            var request = new UpdateLabelCommand(User.Identity?.Name, id, labelDto);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => NoContent(),
                notFound => NotFound()
            );
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLabel([FromRoute]int id)
        {
            var request = new DeleteLabelCommand(User.Identity?.Name, id);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => NoContent(),
                notFound => NotFound()
            );
        }

        #endregion
    }
}
