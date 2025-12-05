using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblRegulation
{
    public int RegulationId { get; set; }

    public string RegulationCode { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? PenaltyPoints { get; set; }

    public decimal? FineAmount { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual TblAccount? CreatedByNavigation { get; set; }
}
