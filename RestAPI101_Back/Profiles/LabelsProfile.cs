using AutoMapper;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Profiles {
    public class LabelsProfile : Profile {
        public LabelsProfile() {
            CreateMap<Label, LabelReadDTO>();
            CreateMap<LabelWriteDTO, Label>();
        }
    }
}