﻿using AutoMapper;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;

namespace Fragments.Domain.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            CreateMap<Notifications, NotificationsDto>().ReverseMap().ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Notifications, NotificationsDto>();
        }
    }
}
