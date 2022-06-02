using AutoMapper;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<ChannelsOfRefference, ChannelsOfRefferenceDTO>().ReverseMap();
        }
    }
}
