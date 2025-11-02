using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblBuilding
{
    public int BuildingId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? TotalFloors { get; set; }

    public int? TotalRooms { get; set; }

    public virtual ICollection<TblFloor> TblFloors { get; set; } = new List<TblFloor>();
}
