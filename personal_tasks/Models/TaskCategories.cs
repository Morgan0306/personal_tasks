using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class TaskCategories
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
}
