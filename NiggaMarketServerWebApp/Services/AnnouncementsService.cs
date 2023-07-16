using Microsoft.EntityFrameworkCore;
using NiggaMarketServerWebApp.DbConnector;
using NiggaMarketServerWebApp.Dtos;
using NiggaMarketServerWebApp.Models;

namespace NiggaMarketServerWebApp.Services;

public class AnnouncementsService
{
    private NiggamarketDbContext _dbContext;

    public AnnouncementsService(NiggamarketDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<AnnouncementCardResponseDto> GetAllAnnouncementsCards()
    {
        List<AnnouncementCardResponseDto> announcements = _dbContext.Announcements
            .Where(item => item.IsActive == true)
            .OrderBy(item => item.Id)
            .Select(
                announcement => new AnnouncementCardResponseDto()
                {
                    Id = announcement.Id,
                    Name = announcement.Name,
                    Age = announcement.Age,
                    Gender = announcement.Gender,
                    Height = announcement.Height,
                    Price = announcement.Price,
                    Weight = announcement.Weight,
                    PhoneNumber = announcement.PhoneNumber,
                    PicturePath = announcement.PicturePath
                }).ToList();

        return announcements;
    }

    public AnnouncementPageResponseDto GetAnnouncementPageById(int id)
    {
        AnnouncementPageResponseDto announcement = _dbContext.Announcements
            .Where(item => item.Id == id)
            .Include(item => item.Comments)
            .Include(item => item.Injuries)
            .Include(item => item.AnnouncementsWorks)
            .Select(
                announcement => new AnnouncementPageResponseDto()
                {
                    Id = announcement.Id,
                    Name = announcement.Name,
                    Age = announcement.Age,
                    Gender = announcement.Gender,
                    Height = announcement.Height,
                    Price = announcement.Price,
                    Weight = announcement.Weight,
                    PhoneNumber = announcement.PhoneNumber,
                    PicturePath = announcement.PicturePath,
                    Comments = announcement.Comments.Select(comment => new CommentResponseDto()
                    {
                        Content = comment.Content,
                        AuthorName = comment.AuthorName
                    }).ToList(),
                    Description = announcement.Description,
                    Injuries = announcement.Injuries.Select(injury => injury.Name).ToList(),
                    AnnouncementsWorks = announcement.AnnouncementsWorks.Select(announcementWork =>
                        new AnnouncementWorkResponseDto()
                        {
                            WorkName = announcementWork.Work.Name,
                            Experience = announcementWork.Experience
                        }).ToList()
                }).First();

        return announcement;
    }
}