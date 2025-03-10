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
        public string RoleId { get; set; }

        [Display(Name = "狀態")]
        public bool IsActive { get; set; }

        // 下拉式選單資料來源
        public IEnumerable<SelectListItem> DepartmentList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> RoleList { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}
