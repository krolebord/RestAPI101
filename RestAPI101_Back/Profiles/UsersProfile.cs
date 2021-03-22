﻿using AutoMapper;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Profiles {
    public class UsersProfile : Profile {
        public UsersProfile() {
            CreateMap<UserLoginDTO, UserRegisterDTO>().ReverseMap();
            
            CreateMap<UserRegisterDTO, User>();
        }
    }
}