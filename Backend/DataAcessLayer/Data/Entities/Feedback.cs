using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class Feedback
{
    public Guid FeedbackId { get; set; }

    public Guid UserId { get; set; }

    public Guid EventId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
