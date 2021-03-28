using AutoMapper;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Profiles {
    public class AuthTokensProfile : Profile {
        public AuthTokensProfile() {
            CreateMap<AuthToken, AuthTokenReadDTO>();
        }
    }
}