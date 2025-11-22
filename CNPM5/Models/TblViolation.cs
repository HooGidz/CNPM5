using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblViolation
{
    public int ViolationId { get; set; }

    public int StudentId { get; set; }

    public int RuleId { get; set; }

    public DateOnly? ViolationDate { get; set; }

    public string? Note { get; set; }

    public virtual TblRule Rule { get; set; } = null!;

    public virtual TblStudent Student { get; set; } = null!;
}
