using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fragments.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("create-notification")]
        public async Task<IActionResult> CreateAsync(NotificationsDto notification)
        {
            return Ok(await _notificationService.AddNotificationAsync(notification));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _notificationService.DeleteNotificationAsync(id);
            return deleted ? Ok() : Forbid();
        }
        [HttpPost("readMessage")]
        public async Task<IActionResult> ReadingTheMessage(NotificationsDto messageDTO)
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
