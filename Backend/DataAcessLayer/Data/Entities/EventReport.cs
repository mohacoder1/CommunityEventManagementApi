using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class EventReport
{
    public Guid EventId { get; set; }

    public string EventTitle { get; set; } = null!;

    public int? Registrations { get; set; }

    public int? CurrentRegistrations { get; set; }

    public decimal? AttendancePercentage { get; set; }

    public int? AverageRating { get; set; }
}
