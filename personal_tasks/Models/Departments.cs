using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class Departments
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int? ManagerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Users? Manager { get; set; }

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
