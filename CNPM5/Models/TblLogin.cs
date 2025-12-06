using System;
using System.Collections.Generic;

namespace CNPM5.Models;

public partial class TblLogin
{
    public int LoginId { get; set; }

    public int StudentId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
