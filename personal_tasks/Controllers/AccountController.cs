﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using personal_tasks.Helpers;
using personal_tasks.Models;
using personal_tasks.ViewModels;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                // 使用 SimpleHash.ComputeHash 作為雜湊方法
                string hashedPassword = SimpleHash.ComputeHash(model.Password, "SHA256", null);

                var user = _db.Users.FirstOrDefault(u => u.UserName == model.Username && u.PasswordHash == hashedPassword);
                if (user != null)
                {
                    // 檢查用戶狀態，如果停用則拒絕登入
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("", "該帳號已停用，無法登入。");
                        return View(model);
                    }
                    // 建立使用者的 Claim 列表
                    var claims = new List<Claim>
                    {
                        // 根據需要加入其他 Claim
                        new Claim(ClaimTypes.Name, user.Name ?? user.UserName),
                        
                        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),

                        new Claim("RoleId", user.RoleId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // 設定 Cookie 的選項，決定是否持久化 Cookie
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        // 設定 Cookie 的過期時間
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
            var model = new RegisterViewModel();

            // 載入部門下拉清單
            model.DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName");

            // 載入角色下拉清單
            model.RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName");

            // 預設：非部門主管
            model.IsDepartmentManager = false;

            // 取得目前登入的使用者資訊（依據 Claim 提供的使用者ID）
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int currentUserId))
            {
                // 取出當前使用者資料
                var currentUser = _context.Users.FirstOrDefault(u => u.UserID == currentUserId);
                if (currentUser != null)
                {
                    // 檢查使用者本身的 RoleId 是否為 6 (代表主管)
                    bool isManagerByRole = (currentUser.RoleId == 6);

                    // 依據使用者的 DepartmentId，檢查對應部門的 ManagerId 是否相等
                    bool isManagerByDept = false;
                    if (currentUser.DepartmentId.HasValue)
                    {
                        var dept = _context.Departments.FirstOrDefault(d => d.DepartmentId == currentUser.DepartmentId.Value);
                        if (dept != null)
                        {
                            isManagerByDept = (dept.ManagerId == currentUser.UserID);
                        }
                    }

                    if (isManagerByRole || isManagerByDept)
                    {
                        // 認定該使用者為部門主管
                        model.IsDepartmentManager = true;

                        // 從 Claims 或目前使用者資料取得部門資訊
                        // 這裡優先從使用者資料中取得 DepartmentId
                        if (currentUser.DepartmentId.HasValue)
                        {
                            model.DepartmentId = currentUser.DepartmentId;
                            var dept = _context.Departments.FirstOrDefault(d => d.DepartmentId == currentUser.DepartmentId.Value);
                            model.DepartmentName = dept?.DepartmentName;
                        }
                        else
                        {
                            // 若 DepartmentId 尚未設定，嘗試從 Claims 取得
                            var deptIdClaim = User.FindFirst("DepartmentId");
                            if (deptIdClaim != null && int.TryParse(deptIdClaim.Value, out int deptId))
                            {
                                model.DepartmentId = deptId;
                                var dept = _context.Departments.FirstOrDefault(d => d.DepartmentId == deptId);
                                model.DepartmentName = dept?.DepartmentName;
                            }
                        }
                        // 部門主管在本系統只能註冊員工
                        model.RoleId = "5";
                    }
                }
            }
            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // 重新初始化下拉選單，否則再次返回 View 時會缺少選單資料
                model.DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName");
                model.RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName");
                return View(model);
            }
            // 檢查是否已有相同的使用者名稱
            var userExists = _context.Users.Any(u => u.UserName == model.Username);
            if (userExists)
            {
                ModelState.AddModelError("", "該使用者名稱已存在，請選擇其他名稱");
                model.DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName");
                model.RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName");
                return View(model);
            }
            // 如果是部門主管操作，強制角色為員工
            if (model.IsDepartmentManager)
            {
                model.RoleId = "5";
            }
            string hashedPassword = SimpleHash.ComputeHash(model.Password, "SHA256", null);
            var newUser = new Users
            {
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = hashedPassword,
                RoleId = int.Parse(model.RoleId), //  RoleId 儲存為 int
                DepartmentId = model.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.Now,
                Name = model.FullName
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            // 如果新使用者是部門主管，則更新對應部門的 ManagerId 為此新使用者的 UserId
            if (newUser.RoleId == 5 && newUser.DepartmentId.HasValue)
            {
                var dept = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == newUser.DepartmentId.Value);
                if (dept != null)
                {
                    dept.ManagerId = newUser.UserID;
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("AccountManagement", "Admin");                        
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
