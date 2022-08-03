using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using Fragments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Fragments.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("readMessage")]
        public async Task <IActionResult> ReadingTheMessage (NotificationsDTO messageDTO)
        {
            await _notificationService.ReadingTheMessage(messageDTO);
            return Ok();
        }

        [HttpGet("getNotifications")]
        public async Task<IActionResult> GetNotificationsWithCorrectUser(bool sortingBy, int pageIndex, bool typeOfRead)
        {
            //var _u = _userService.GetMe();
            
            return Ok(await _notificationService.GetNotificationsAsync(sortingBy, pageIndex, typeOfRead));
        }
    }
}
