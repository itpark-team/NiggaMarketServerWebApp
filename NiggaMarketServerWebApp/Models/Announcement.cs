using System;
using System.Collections.Generic;

namespace NiggaMarketServerWebApp.Models;

public partial class Announcement
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

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AnnouncementWork> AnnouncementsWorks { get; set; } = new List<AnnouncementWork>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; }

    public virtual ICollection<Injury> Injuries { get; set; } = new List<Injury>();
}
