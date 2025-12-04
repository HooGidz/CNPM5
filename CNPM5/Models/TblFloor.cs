using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblFloor
{
    public int FloorId { get; set; }

    public int BuildingId { get; set; }

    public int FloorNumber { get; set; }

    public virtual TblBuilding Building { get; set; } = null!;

    public virtual ICollection<TblRoom> TblRooms { get; set; } = new List<TblRoom>();
}
