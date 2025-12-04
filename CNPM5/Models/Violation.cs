using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class Violation
{
    public int ViolationId { get; set; }

    public int StudentId { get; set; }

    public int RuleId { get; set; }

    public DateTime? ViolationDate { get; set; }

    public string? Note { get; set; }

    public virtual Rule Rule { get; set; } = null!;

    public virtual TblStudents Student { get; set; } = null!;
}
