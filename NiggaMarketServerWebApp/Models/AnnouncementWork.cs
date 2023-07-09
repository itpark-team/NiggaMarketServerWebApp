using System;
using System.Collections.Generic;

namespace NiggaMarketServerWebApp.Models;

public partial class AnnouncementWork
{
    public int AnnouncementId { get; set; }

    public int WorkId { get; set; }

    public int Experience { get; set; }

    public virtual Announcement Announcement { get; set; }

    public virtual Work Work { get; set; }
}
