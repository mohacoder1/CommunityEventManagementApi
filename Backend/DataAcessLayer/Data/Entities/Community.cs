using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class Community
{
    public Guid CommunityId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
