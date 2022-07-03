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

        public UserService(IFragmentsContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configiguration)
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

        public async Task CreateAsync(UserDTO user)
        {
            var userInfo = _mapper.Map<User>(user);

            await _context.Users.AddAsync(userInfo);

            await _context.SaveChangesAsync();
        }
        public async Task<AuthenticateResponseDTO> LoginAsync(AuthenticateRequestDTO model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == model.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var token = _configuration.GenerateJwtToken(user);

            return new AuthenticateResponseDTO(user, token);
        }

        public async Task<UserDTO> GetMe()
        {
            var result = string.Empty;
            var response = new UserDTO();
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == int.Parse(result));
                response = _mapper.Map<UserDTO>(user);
            }
            return response;
        }

        public async Task<UserDTO> GetAsync(int id)
        {

            var user = await _context.Users.FindAsync(id);
            var userInfo = _mapper.Map<UserDTO>(user);

            if (userInfo == null)
            {
                throw new Exception("Not Found");
            }

            return userInfo;
        }
    }
}
