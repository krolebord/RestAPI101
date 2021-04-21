using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers
{
    [ApiController]
    [Route(APIRoutes.LabelsController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class LabelsController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILabelsRepository _labelsRepository;

        public LabelsController(ILabelsRepository labelsRepository, IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
            this._labelsRepository = labelsRepository;
        }

        // GET api/labels
        [HttpGet(APIRoutes.Labels.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<LabelReadDTO>> GetAllLabels()
        {
            var user = _usersRepository.GetUserByLogin(User.Identity!.Name!)!;

            var labels = user.Labels;

            if (!labels.Any())
                return NoContent();

            var mappedLabels = labels.Select(label => label.ToReadDTO());
            return Ok(mappedLabels);
        }

        // GET api/labels/{id}
        [HttpGet(APIRoutes.Labels.GetSpecified, Name = nameof(GetLabelById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LabelReadDTO> GetLabelById(int id)
        {
            var user = GetUser();
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if(label == null)
                return NotFound();

            return Ok(label.ToReadDTO());
        }

        // POST api/labels
        [HttpPost(APIRoutes.Labels.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult CreateLabel(LabelWriteDTO labelDto)
        {
            var user = GetUser();
            var label = labelDto.ToLabel();
            label.User = user;
            _labelsRepository.CreateLabel(label);

            _labelsRepository.SaveChanges();

            var readDto = label.ToReadDTO();

            // ReSharper disable once RedundantAnonymousTypePropertyName
            return CreatedAtRoute(nameof(GetLabelById), new { Id = readDto.Id }, readDto);
        }

        // PUT api/labels/{id}
        [HttpPut(APIRoutes.Labels.Update)]
        public ActionResult UpdateLabel(int id, LabelWriteDTO labelDto)
        {
            var user = GetUser();
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if (label == null)
                return NotFound();

            label.MapWriteDTO(labelDto);

            _labelsRepository.SaveChanges();

            return NoContent();
        }

        // DELETE api/labels/{id}
        [HttpDelete(APIRoutes.Labels.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LabelReadDTO> DeleteLabel(int id)
        {
            var user = GetUser();
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if (label == null)
                return NotFound();

            _labelsRepository.DeleteLabel(label);
            _labelsRepository.SaveChanges();

            return Ok(label.ToReadDTO());
        }

        private User GetUser() =>
            _usersRepository.GetUserByLogin(User.Identity!.Name!)!;
    }
}
