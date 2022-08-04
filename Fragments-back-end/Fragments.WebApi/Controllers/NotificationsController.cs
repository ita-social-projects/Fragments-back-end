using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task <IActionResult> ReadingTheMessage (NotificationsDto messageDTO)
        {
            await _notificationService.ReadingTheMessage(messageDTO);
            return Ok();
        }

        [HttpGet("getNotifications")]
        public async Task<IActionResult> GetNotificationsWithCorrectUser(bool sortingBy, bool typeOfRead)
        {
            return Ok(await _notificationService.GetNotificationsAsync(sortingBy, typeOfRead));
        }
    }
}
