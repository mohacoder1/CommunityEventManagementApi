using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class Registration
{
    public Guid RegistrationId { get; set; }

    public Guid EventId { get; set; }

    public Guid MemberId { get; set; }

    public DateTime RequestedAt { get; set; }

    public byte[]? TokenHash { get; set; }

    public string Status { get; set; } = null!;

    public virtual CheckIn? CheckIn { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual CommunityMember Member { get; set; } = null!;
}
