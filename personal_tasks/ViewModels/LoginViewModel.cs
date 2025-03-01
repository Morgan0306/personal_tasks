using System.ComponentModel.DataAnnotations;

namespace personal_tasks.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入使用者名稱")]
        [Display(Name = "使用者名稱")]
        public string Username { get; set; }

        // 由使用者輸入的密碼，使用 DataType.Password 可使表單控件隱藏字元
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        //// 可選擇性欄位 - 記住我
        //[Display(Name = "記住我？")]
        //public bool RememberMe { get; set; }
    }
}
