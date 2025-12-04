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

<<<<<<< HEAD
    public virtual ICollection<TblRegulation> TblRegulations { get; set; } = new List<TblRegulation>();

    public virtual ICollection<TblStudents> TblStudents { get; set; } = new List<TblStudents>();

=======
    

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
>>>>>>> d96acfd2a49552565f66cc66a4024acb7b221595
}
