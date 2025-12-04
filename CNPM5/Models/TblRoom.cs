using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblRoom
{
    public int RoomId { get; set; }

    public int FloorId { get; set; }

    public string RoomName { get; set; } = null!;

    public int Capacity { get; set; }

    public int? CurrentOccupants { get; set; }

    public string? Gender { get; set; }

    public string? RoomType { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public virtual TblFloor Floor { get; set; } = null!;

   

    public virtual ICollection<TblServiceUsage> TblServiceUsages { get; set; } = new List<TblServiceUsage>();
}
