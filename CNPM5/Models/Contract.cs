using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int StudentId { get; set; }

    public int RoomId { get; set; }

    public int ServiceId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? Deposit { get; set; }

    public decimal MonthlyFee { get; set; }

    public DateOnly Cycle { get; set; }

    public DateOnly Harvestday { get; set; }

    public string? Status { get; set; }

   

    public DateTime? CreatedDate { get; set; }

    public String? CreatedBy { get; set; }
    public virtual TblRoom Room { get; set; } = null!;
    public virtual TblService Service { get; set; } = null!;    
    public virtual TblStudents Student { get; set; } = null!;
}
