using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.Data.RepositoryExtensions;
using RestAPI101.Domain.DTOs.Label;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;
using RestAPI101.WebAPI.Filters;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.LabelsController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class LabelsController : ControllerBase
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Label> _labelsRepository;

        public LabelsController(IRepository<Label> labelsRepository, IRepository<User> usersRepository)
        {
            this._usersRepository = usersRepository;
            this._labelsRepository = labelsRepository;
        }

        [HttpGet]
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

        [HttpGet("{id:int}")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult CreateLabel(LabelWriteDTO labelDto)
        {
            var user = GetUser();
            var label = labelDto.ToLabel();
            label.User = user;
            _labelsRepository.Add(label);

            _labelsRepository.SaveChanges();

            var readDto = label.ToReadDTO();

            // ReSharper disable once RedundantAnonymousTypePropertyName
            return CreatedAtRoute(nameof(GetLabelById), new { Id = readDto.Id }, readDto);
        }

        [HttpPut("{id:int}")]
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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LabelReadDTO> DeleteLabel(int id)
        {
            var user = GetUser();
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if (label == null)
                return NotFound();

            _labelsRepository.Delete(label);
            _labelsRepository.SaveChanges();

            return Ok(label.ToReadDTO());
        }

        private User GetUser() =>
            _usersRepository.GetUserByLogin(User.Identity!.Name!)!;
    }
}
