using System.ComponentModel.DataAnnotations;

namespace personal_tasks.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "請輸入使用者名稱")]
        [StringLength(50)]
        [Display(Name = "使用者名稱")]
        public string Username { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密碼至少需6個字元")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請再輸入一次密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密碼與確認密碼不符")]
        [Display(Name = "確認密碼")]
        public string ConfirmPassword { get; set; }
    }
}
