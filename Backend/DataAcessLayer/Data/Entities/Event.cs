using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class Event
{
    public Guid EventId { get; set; }

    public Guid CommunityId { get; set; }

    public Guid OrganizedId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Capacity { get; set; }

    public int CurrentRegisteredCount { get; set; }

    public string Status { get; set; } = null!;

    public virtual Community Community { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual CommunityMember Organized { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
