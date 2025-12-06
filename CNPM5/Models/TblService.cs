using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblService
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<TblServiceUsage> TblServiceUsages { get; set; } = new List<TblServiceUsage>();
}
