using AutoMapper;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Fragments.Domain.Services.Interfaces;
using Fragments.Domain.Extensions;

namespace Fragments.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExtensionsWrapper _wrapper;
        public UserService(
            IFragmentsContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configiguration
            )
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _wrapper = new ExtensionsWrapper(configiguration);
        }

        public async Task<bool> IsEmailAlreadyExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task CreateAsync(UserDto user)
        {
            var userInfo = _mapper.Map<User>(user);
            if (!(await _context.Users.AnyAsync(u => u.Email == user.Email)))
            {    
                AddWelcomeNotification(userInfo);

                await _context.Users.AddAsync(userInfo);

                await _context.SaveChangesAsync();
            }
        }
        public async Task<AuthenticateResponseDto> LoginAsync(AuthenticateRequestDto model)
        {
            var user = await _context.Users.FirstAsync(user => user.Email == model.Email);
            var token = _wrapper.GetJwtToken(user);
            _context.Entry(user).State = EntityState.Modified;
            user.LastActivityDate = DateTime.UtcNow;
            return new AuthenticateResponseDto(user, token);
        }

        public async Task<UserDto> GetMeAsync()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue("id");
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == int.Parse(result));
                var response = _mapper.Map<UserDto>(user);
                return response;
            }
            return null!;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == id);

            var userInfo = _mapper.Map<UserDto>(user);

            return userInfo;
        }
        private static void AddWelcomeNotification(User user) 
        {
            user.Notifications = new List<Notifications> { new Notifications  {
                Theme = "Вітання у Fragmenty",

                Body = "Вітаємо у Спільноті Fragmenty! " +
            "Ви можете підтримати платформу, створити проект або долучитись до існуючих проектів",

                Date = DateTime.Now,

                }
            };
        }
        public async Task UpdateAsync(UserDto user)
        {
            var existingUser = _context.Users
            .Where(p => p.Id == user.Id)
            .Include(p => p.ChannelsOfRefferences)
             .Single();

            if (existingUser != null
                && existingUser.ChannelsOfRefferences != null
                && user.ChannelsOfRefferences != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);

                foreach (var existingChannel in existingUser.ChannelsOfRefferences.ToList())
                {
                    RemoveChannelsOfRefference(user, existingChannel);
                }
                foreach (var channel in user.ChannelsOfRefferences)
                {
                    if (channel.ChannelId != 0)
                    {
                        var existingChannel = existingUser.ChannelsOfRefferences
                        .SingleOrDefault(c => c.ChannelId == channel.ChannelId);
                        SetChannelValues(existingChannel!, channel);
                    }
                    else
                    {

                        var newChannel = new ChannelsOfRefference
                        {
                            ChannelDetails = channel.ChannelDetails,
                            ChannelName = channel.ChannelName,
                            UserId = existingUser.Id,
                        };
                        existingUser.ChannelsOfRefferences.Add(newChannel);
                    }
                }

                await _context.SaveChangesAsync();
            }

        }
        public bool SetChannelValues(ChannelsOfRefference existingChannel, ChannelsOfRefferenceDto channel)
        {
            if (existingChannel != null)
            {
                _context.Entry(existingChannel).CurrentValues.SetValues(channel);
            }
            return true;
        }
        public bool RemoveChannelsOfRefference(UserDto user, ChannelsOfRefference existingChannel)
        {
            if (user != null
               && user.ChannelsOfRefferences != null
               && user.ChannelsOfRefferences != null
               && !user.ChannelsOfRefferences.Any(c => c.ChannelId == existingChannel.ChannelId))
            {
                    _context.ChannelsOfRefferences.Remove(existingChannel);
            }
            return true;
        }
       
    }
}
