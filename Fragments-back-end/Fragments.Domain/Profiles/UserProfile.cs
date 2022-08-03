using AutoMapper;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using System.Collections.Generic;

namespace Fragments.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, UserDTO>();

            CreateMap<ChannelsOfRefference, ChannelsOfRefferenceDTO>().ReverseMap();
            
            CreateMap<ChannelsOfRefference, ChannelsOfRefferenceDTO>();

            CreateMap<User, AuthenticateRequestDTO>().ReverseMap();

            CreateMap<Notifications, NotificationsDTO>().ReverseMap();

            CreateMap<Notifications, NotificationsDTO>();
        }
    }
}
