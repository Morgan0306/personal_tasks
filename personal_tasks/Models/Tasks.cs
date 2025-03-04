using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class Tasks
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int CreatedBy { get; set; }

    public int? DepartmentId { get; set; }

    public int? CategoryId { get; set; }

    public int? ParentTaskId { get; set; }

    public string Status { get; set; } = null!;

    public int Priority { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual TaskCategories? Category { get; set; }

    public virtual Users CreatedByNavigation { get; set; } = null!;

    public virtual Departments? Department { get; set; }

    public virtual ICollection<Tasks> InverseParentTask { get; set; } = new List<Tasks>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public virtual Tasks? ParentTask { get; set; }

    public virtual ICollection<TaskDelegations> TaskDelegations { get; set; } = new List<TaskDelegations>();

    public virtual ICollection<TaskHistory> TaskHistory { get; set; } = new List<TaskHistory>();

    public virtual ICollection<askComments> askComments { get; set; } = new List<askComments>();
}
