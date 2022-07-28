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
        private readonly INotificationService _notificationsService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationsService = notificationService;
        }

        [HttpPost("create-notification")]
        public async Task<IActionResult> CreateAsync(NotificationsDTO notification)
        {
            return Ok(await _notificationsService.AddNotificationAsync(notification));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _notificationsService.DeleteNotificationAsync(id);
            return deleted ? Ok() : Forbid();
        }
        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotificationsAsync()
        {
            return Ok(await _notificationsService.GetNotificationsAsync());
        }
    }
}
