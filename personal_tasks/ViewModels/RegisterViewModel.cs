using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace personal_tasks.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "帳號是必填的")]
        [Display(Name = "帳號")]
        public string Username { get; set; }

        [Required(ErrorMessage = "密碼是必填的")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "確認密碼是必填的")]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼與確認密碼不相符")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email 是必填的")]
        [EmailAddress(ErrorMessage = "請輸入正確的 Email 格式")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "姓名是必填的")]
        [Display(Name = "姓名")]
        public string FullName { get; set; }

        // 部門選單：若使用者為主要管理者，則需要從下拉式選單中選擇部門；
        // 若是部門主管，則自動指定該部門，部門名稱亦會顯示供參考
        [Display(Name = "部門")]
        public int? DepartmentId { get; set; }

        // 若是部門主管註冊帳號，則傳入其預設部門名稱，供 readonly 使用者參考
        public string DepartmentName { get; set; }

        // 提供下拉選單使用，內容例如「人事」、「會計」等
        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        // 權限分級選單：主要管理者可以下拉選擇老闆、部門主管、員工；
        // 部門主管則只能建立員工帳號，所以 RoleId 固定為「員工」
        [Display(Name = "權限分級")]
        public string RoleId { get; set; }

        // 提供下拉選單使用，內容例如「老闆」、「部門主管」、「員工」
        public IEnumerable<SelectListItem> RoleList { get; set; }

        // 指示當前註冊操作是否由部門主管執行（若為 true，則部門與角色欄位均固定）
        public bool IsDepartmentManager { get; set; }
    }
}
