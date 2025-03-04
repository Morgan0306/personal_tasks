using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using personal_tasks.Helpers;
using personal_tasks.Models;
using personal_tasks.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace personal_tasks.Controllers
{
    public class AccountController : Controller
    {
        private readonly Personal_TasksContext _db;
        private readonly Personal_TasksContext _context;
        public AccountController(Personal_TasksContext db, Personal_TasksContext context)
        {
            _db = db;
            _context = context;
        }

        /// <summary>
        /// GET: /Account/Login
        /// 用來呈現登入頁面
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/Login
        /// 驗證使用者的登入資料，設定認證 Cookie 或返回錯誤訊息
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 這裡使用 SimpleHash.ComputeHash 作為雜湊方法
                string hashedPassword = SimpleHash.ComputeHash(model.Password, "SHA256", null);

                var user = _db.Users.FirstOrDefault(u => u.UserName == model.Username && u.PasswordHash == hashedPassword);
                if (user != null)
                {
                    // 建立使用者的 Claim 列表
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name ?? user.UserName),
                        // 根據需要可以加入其他 Claim
                        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),

                        new Claim("RoleId", user.RoleId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // 設定 Cookie 的選項，這裡根據 RememberMe 決定是否持久化 Cookie
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        // 可設定 Cookie 的過期時間
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    // 發行 Cookie，將使用者登入狀態寫入 HttpContext
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "使用者名稱或密碼不正確");
                }
            }
            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 檢查是否已有相同的使用者名稱
                var userExists = _context.Users.Any(u => u.UserName == model.Username);
                if (userExists)
                {
                    ModelState.AddModelError("", "該使用者名稱已存在，請選擇其他名稱");
                    return View(model);
                }

                // 利用 SimpleHash 計算密碼雜湊值
                string hashedPassword = SimpleHash.ComputeHash(model.Password, "SHA256", null);

                // 建立新使用者物件
                var newUser = new Users
                {
                    UserName = model.Username,
                    PasswordHash = hashedPassword,
                    RoleId = 0   // 預設最低權限，可依需求修改
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // 註冊成功後，可以選擇自動登入，或導向登入頁面
                // 這裡採用導向登入頁面的做法
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // 透過 Cookie 驗證模型登出
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // 登出後導向首頁
            return RedirectToAction("Index", "Home");
        }

    }
}
