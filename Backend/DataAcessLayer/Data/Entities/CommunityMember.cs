using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class CommunityMember
{
    public Guid CommunityMemberId { get; set; }

    public Guid CommunityId { get; set; }

    public Guid UserId { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual Community Community { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual User User { get; set; } = null!;
}
