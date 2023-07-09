namespace NiggaMarketServerWebApp.Dtos;

public class AnnouncementPageResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public int Gender { get; set; }

    public int Weight { get; set; }

    public int Height { get; set; }

    public string Description { get; set; }

    public string PicturePath { get; set; }

    public int Price { get; set; }

    public string PhoneNumber { get; set; }

    public List<AnnouncementWorkResponseDto> AnnouncementsWorks { get; set; }

    public List<CommentResponseDto> Comments { get; set; }

    public List<String> Injuries { get; set; }
}