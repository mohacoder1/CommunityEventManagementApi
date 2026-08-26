using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class CheckIn
{
    public Guid CheckInId { get; set; }

    public Guid RegistrationId { get; set; }

    public DateTime CheckedAt { get; set; }

    public Guid? VerifiedBy { get; set; }

    public virtual Registration Registration { get; set; } = null!;

    public virtual CommunityMember? VerifiedByNavigation { get; set; }
}
