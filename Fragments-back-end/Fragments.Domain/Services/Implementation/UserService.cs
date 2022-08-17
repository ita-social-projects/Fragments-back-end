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

            AddWelcomeNotification(userInfo);

            await _context.Users.AddAsync(userInfo);

            await _context.SaveChangesAsync();
        }
        public async Task<AuthenticateResponseDto> LoginAsync(AuthenticateRequestDto model)
        {
            var user = await _context.Users.FirstAsync(user => user.Email == model.Email);

            var token = _wrapper.GetJwtToken(user);
            
            return new AuthenticateResponseDto(user, token);
        }

        public async Task<UserDto> GetMeAsync()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
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
    }
}
