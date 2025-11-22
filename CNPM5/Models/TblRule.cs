using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblRule
{
    public int RuleId { get; set; }

    public string RuleName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Penalty { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<TblViolation> TblViolations { get; set; } = new List<TblViolation>();
}
