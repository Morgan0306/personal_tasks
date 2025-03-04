using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class askComments
{
    public int CommentId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tasks Task { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
