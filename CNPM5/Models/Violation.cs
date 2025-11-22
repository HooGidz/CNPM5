using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CNPM5.Models;

public partial class Violation
{
    public int ViolationId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn Sinh viên.")]
    public int? StudentId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn Nội quy vi phạm.")]
    public int? RuleId { get; set; }

    public DateTime ViolationDate { get; set; }

    public string? Note { get; set; }

    public virtual Rule Rule { get; set; } = null!;

    public virtual TblStudent Student { get; set; } = null!;
}
