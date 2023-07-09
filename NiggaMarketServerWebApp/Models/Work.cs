using System;
using System.Collections.Generic;

namespace NiggaMarketServerWebApp.Models;

public partial class Work
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<AnnouncementWork> AnnouncementsWorks { get; set; } = new List<AnnouncementWork>();
}
