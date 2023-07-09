using System;
using System.Collections.Generic;

namespace NiggaMarketServerWebApp.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string AuthorName { get; set; }

    public string Content { get; set; }

    public int AnnouncementId { get; set; }

    public virtual Announcement Announcement { get; set; }
}
