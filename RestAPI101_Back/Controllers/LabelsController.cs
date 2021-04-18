using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController] 
    [Route(APIRoutes.LabelsController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class LabelsController : ControllerBase {
        private readonly IUsersRepository usersRepository;
        private readonly ILabelsRepository labelsRepository;
        private readonly IMapper mapper;
        
        public LabelsController(ILabelsRepository labelsRepository, IUsersRepository usersRepository, IMapper mapper) {
            this.usersRepository = usersRepository;
            this.labelsRepository = labelsRepository;
            this.mapper = mapper;
        }
        
        // GET api/labels
        [HttpGet(APIRoutes.Labels.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<LabelReadDTO>> GetAllLabels() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);

            var labels = user.Labels;

            if (labels == null || !labels.Any())
                return NoContent();
            
            var mappedLabels = mapper.Map<IEnumerable<LabelReadDTO>>(labels);
            return Ok(mappedLabels);
        }
        
        // GET api/labels/{id}
        [HttpGet(APIRoutes.Labels.GetSpecified, Name = nameof(GetLabelById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LabelReadDTO> GetLabelById(int id) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if(label == null)
                return NotFound();
            
            return Ok(mapper.Map<Label, LabelReadDTO>(label));
        }
        
        // POST api/labels
        [HttpPost(APIRoutes.Labels.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult CreateLabel(LabelWriteDTO labelDto) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var label = mapper.Map<LabelWriteDTO, Label>(labelDto);
            label.User = user;
            labelsRepository.CreateLabel(label);

            labelsRepository.SaveChanges();

            var readDto = mapper.Map<Label, LabelReadDTO>(label);
            
            return CreatedAtRoute(nameof(GetLabelById), new { Id = readDto.Id }, readDto);
        }
        
        // PUT api/labels/{id}
        [HttpPut(APIRoutes.Labels.Update)]
        public ActionResult UpdateLabel(int id, LabelWriteDTO labelDto) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if (label == null)
                return NotFound();
            
            mapper.Map<LabelWriteDTO, Label>(labelDto, label);
            
            labelsRepository.SaveChanges();

            return NoContent();
        }
        
        // DELETE api/labels/{id}
        [HttpDelete(APIRoutes.Labels.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LabelReadDTO> DeleteLabel(int id) {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            var label = user.Labels.FirstOrDefault(x => x.Id == id);

            if (label == null)
                return NotFound();
            
            labelsRepository.DeleteLabel(label);
            labelsRepository.SaveChanges();

            return Ok(mapper.Map<LabelReadDTO>(label));
        }
    }
}