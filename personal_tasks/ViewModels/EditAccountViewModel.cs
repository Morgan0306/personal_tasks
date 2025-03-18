using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace personal_tasks.ViewModels
{
    public class EditAccountViewModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Email 是必填的")]
        [EmailAddress(ErrorMessage = "請輸入正確的 Email 格式")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "姓名是必填的")]
        [Display(Name = "姓名")]
        public string FullName { get; set; }

        [Display(Name = "部門")]
        public int? DepartmentId { get; set; }

        [Display(Name = "權限")]
        public string? RoleId { get; set; }

        [Display(Name = "狀態")]
        public bool IsActive { get; set; }

        // 帳號僅用於顯示，不允許修改
        [Display(Name = "帳號")]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("NewPassword", ErrorMessage = "密碼和確認密碼不匹配。")]
        public string? ConfirmPassword { get; set; }

        // 下拉選單資料來源
        public IEnumerable<SelectListItem> DepartmentList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> RoleList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 只有在用戶有打算更新密碼時才驗證兩者是否相符
            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (NewPassword != ConfirmPassword)
                {
                    yield return new ValidationResult("密碼和確認密碼不匹配。", new[] { "ConfirmPassword" });
                }
            }
        }
    }
}
