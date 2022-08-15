﻿using AutoMapper;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using System.Collections.Generic;

namespace Fragments.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserDto>();

            CreateMap<ChannelsOfRefference, ChannelsOfRefferenceDto>().ReverseMap();
            
            CreateMap<ChannelsOfRefference, ChannelsOfRefferenceDto>();

            CreateMap<User, AuthenticateRequestDTO>().ReverseMap();

            CreateMap<User,AdminDTO>().ReverseMap();

            CreateMap<User,AdminDTO>();
        }
    }
}
