using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblServiceUsage
{
    public int ServiceUsageId { get; set; }

    public int RoomId { get; set; }

    public int ServiceId { get; set; }

    public DateOnly MonthStart { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? TotalCost { get; set; }

    public bool? IsBilled { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TblRoom Room { get; set; } = null!;

    public virtual TblService Service { get; set; } = null!;
}
