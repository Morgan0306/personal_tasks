using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace personal_tasks.Models;

public partial class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

    [Required(ErrorMessage = "使用者名稱為必填")]
    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "密碼為必填")]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public byte Permissions { get; set; } = 0;
}
