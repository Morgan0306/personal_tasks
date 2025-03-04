using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class TaskDelegations
{
    public int DelegationId { get; set; }

    public int TaskId { get; set; }

    public int AssignedBy { get; set; }

    public int AssignedTo { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DelegatedAt { get; set; }

    public DateTime? RespondedAt { get; set; }

    public virtual Users AssignedByNavigation { get; set; } = null!;

    public virtual Users AssignedToNavigation { get; set; } = null!;

    public virtual Tasks Task { get; set; } = null!;
}
