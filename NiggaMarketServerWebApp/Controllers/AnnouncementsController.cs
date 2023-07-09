using Microsoft.AspNetCore.Mvc;
using NiggaMarketServerWebApp.Dtos;
using NiggaMarketServerWebApp.Services;

namespace NiggaMarketServerWebApp.Controllers;

[ApiController]
[Route("announcements/")]
public class AnnouncementsController : ControllerBase
{
    private AnnouncementsService _announcementsService;

    public AnnouncementsController(AnnouncementsService announcementsService)
    {
        _announcementsService = announcementsService;
    }

    [HttpGet("get-all-cards")]
    public List<AnnouncementCardResponseDto> GetAllAnnouncementsCards()
    {
        return _announcementsService.GetAllAnnouncementsCards();
    }

    [HttpGet("get-page-by-id/{id}")]
    public AnnouncementPageResponseDto GetAnnouncementPageById(int id)
    {
        return _announcementsService.GetAnnouncementPageById(id);
    }
}