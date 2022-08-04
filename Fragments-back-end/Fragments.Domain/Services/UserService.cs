using AutoMapper;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


namespace Fragments.Domain.Services
{
    public class UserService:IUserService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

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
            _configuration = configiguration;
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

            var token = _configuration.GenerateJwtToken(user);
            
            return new AuthenticateResponseDTO(user, token);
        }

        public async Task<UserDTO> GetMeAsync()
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
            var user = await _context.Users.FindAsync(id);
            var userInfo = _mapper.Map<UserDto>(user);

            if (userInfo == null)
            {
                throw new Exception("Not Found");
            }

            return userInfo;
        }
        private void AddWelcomeNotification(User user) 
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
