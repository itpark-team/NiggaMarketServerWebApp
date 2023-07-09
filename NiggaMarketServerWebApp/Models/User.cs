using System;
using System.Collections.Generic;

namespace NiggaMarketServerWebApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
}
