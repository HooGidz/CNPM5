using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CNPM5.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int StudentId { get; set; }

    public int RoomId { get; set; }

    public int ServiceId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public decimal? Deposit { get; set; }

    public decimal MonthlyFee { get; set; }

    public DateTime Cycle { get; set; }
    public DateTime Harvestday { get; set; }

    public string? Status { get; set; }

    public int AccountId { get; set; }

    public DateTime? CreatedDate { get; set; }

    [ForeignKey("RoomId")]
    public virtual TblRoom Room { get; set; } = null!;

    [ForeignKey("StudentId")]
    public virtual TblStudents Student { get; set; } = null!;

    [ForeignKey("ServiceId")]
    public virtual TblService Service { get; set; } = null!;

    [ForeignKey("AccountId")]
    public virtual TblAccount? Account { get; set; }
}
