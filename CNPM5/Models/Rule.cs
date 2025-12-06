using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class Rule
{
    public int RuleId { get; set; }

    public string RuleName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Penalty { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public string? Status { get; set; }

    public String CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

   

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
