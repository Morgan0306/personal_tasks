using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class Notifications
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }

    public string NotificationType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tasks Task { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
