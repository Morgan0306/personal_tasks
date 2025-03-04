using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class Roles
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
