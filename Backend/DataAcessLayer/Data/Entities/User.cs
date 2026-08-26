using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public byte[] PhoneNumber { get; set; } = null!;

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
