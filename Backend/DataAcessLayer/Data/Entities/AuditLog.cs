using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class AuditLog
{
    public Guid AuditLogId { get; set; }

    public Guid UserId { get; set; }

    public string Action { get; set; } = null!;

    public string EntityEfffected { get; set; } = null!;

    public DateTime AtTime { get; set; }

    public virtual User User { get; set; } = null!;
}
