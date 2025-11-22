using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Faculty { get; set; }

    public string? Major { get; set; }

    public string? CitizenId { get; set; }

    public string? PermanentAddress { get; set; }

    public string? TemporaryAddress { get; set; }

    public string? AvatarUrl { get; set; }

    public string? StudentStatus { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? AccountId { get; set; }

    public virtual TblAccount? Account { get; set; }

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
