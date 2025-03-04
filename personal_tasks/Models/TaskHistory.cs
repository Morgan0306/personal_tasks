using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class TaskHistory
{
    public int HistoryId { get; set; }

    public int TaskId { get; set; }

    public int ChangedBy { get; set; }

    public string ChangeDescription { get; set; } = null!;

    public DateTime ChangedAt { get; set; }

    public virtual Users ChangedByNavigation { get; set; } = null!;

    public virtual Tasks Task { get; set; } = null!;
}
