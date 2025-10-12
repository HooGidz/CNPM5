using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblAccount
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int RoleId { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? Status { get; set; }
}
