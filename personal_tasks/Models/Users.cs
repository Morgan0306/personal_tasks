using System;
using System.Collections.Generic;

namespace personal_tasks.Models;

public partial class Users
{
    public int UserID { get; set; }

    /// <summary>
    /// 使用者帳號
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// 雜湊後密碼
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// 權限類別
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// 部門
    /// </summary>
    public int? DepartmentId { get; set; }

    /// <summary>
    /// 是否在職
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 創立時間
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更改時間
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// 使用者名字
    /// </summary>
    public string? Name { get; set; }

    public virtual Departments? Department { get; set; }

    public virtual ICollection<Departments> Departments { get; set; } = new List<Departments>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public virtual Roles Role { get; set; } = null!;

    public virtual ICollection<TaskDelegations> TaskDelegationsAssignedByNavigation { get; set; } = new List<TaskDelegations>();

    public virtual ICollection<TaskDelegations> TaskDelegationsAssignedToNavigation { get; set; } = new List<TaskDelegations>();

    public virtual ICollection<TaskHistory> TaskHistory { get; set; } = new List<TaskHistory>();

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

    public virtual ICollection<askComments> askComments { get; set; } = new List<askComments>();
}
