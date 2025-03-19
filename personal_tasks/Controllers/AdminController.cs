using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personal_tasks.Helpers;
using personal_tasks.Models;
using personal_tasks.ViewModels;
using System.Security.Claims;

namespace personal_tasks.Controllers
{
    public class AdminController : Controller
    {
        private readonly Personal_TasksContext _context;

        public AdminController(Personal_TasksContext context)
        {
            _context = context;
        }

        [Authorize] //分頁控制及權限管理
        public async Task<IActionResult> AccountManagement(int page = 1, int? departmentId = null, int? roleId = null)
        {
            int pageSize = 15;
            // 建立基本查詢，包含所需的導覽屬性
            var query = _context.Users
                                .Include(u => u.Role)
                                .Include(u => u.Department)
                                .OrderBy(u => u.UserID)
                                .AsQueryable();

            // 應用篩選條件
            if (departmentId.HasValue)
            {
                query = query.Where(u => u.DepartmentId == departmentId.Value);
            }
            if (roleId.HasValue)
            {
                query = query.Where(u => u.RoleId == roleId.Value);
            }

            int totalUsers = await query.CountAsync();
            var users = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            // 初始化 ViewModel：同時傳遞 Users 清單、分頁資訊，以及及下拉選單資料（部門與角色的選項）
            var model = new AccountManagementViewModel
            {
                Users = users,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize),
                DepartmentFilter = departmentId,  // 可選篩選的部門ID
                RoleFilter = roleId               // 可選篩選的角色ID
            };

            // 下拉選單資料透過 ViewBag 傳遞
            ViewBag.DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName", departmentId);
            ViewBag.RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName", roleId);

            // 設定目前登入使用者相關權限資訊
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int currentUserId = 0;
            if (userIdClaim != null)
            {
                int.TryParse(userIdClaim.Value, out currentUserId);
            }
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == currentUserId);
            if (currentUser != null)
            {
                if (currentUser.RoleId == 7) // 假設 7 = Admin
                    ViewBag.CurrentUserRole = "Admin";
                else if (currentUser.RoleId == 6) // 假設 6 = DepartmentManager
                    ViewBag.CurrentUserRole = "DepartmentManager";
                else
                    ViewBag.CurrentUserRole = "Other";
                ViewBag.CurrentUserDeptId = currentUser.DepartmentId ?? 0;
            }

            return View(model);
        }


        [HttpGet]   //刪除帳號
        public IActionResult DeleteAccount(int id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [HttpPost]  //刪除帳號
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAccountConfirmed(int userID) 
        {
            var user = _context.Users.Find(userID);
            if (user == null)
            {
                return NotFound();
            }

            // 尋找所有部門，其 ManagerId 值與該使用者相同
            var departments = _context.Departments.Where(d => d.ManagerId == user.UserID).ToList();
            foreach (var dept in departments)
            {
                // 解除部門主管關聯，或更新為其他值
                dept.ManagerId = null;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("AccountManagement", "Admin");
        }

        [Authorize]     //編輯帳號
        [HttpGet]
        public async Task<IActionResult> EditAccount(int id)
        {
            // 根據 id 從資料庫中查詢對應使用者，包含相關導覽屬性（如 Role 與 Department）
            var user = await _context.Users
                            .Include(u => u.Role)
                            .Include(u => u.Department)
                            .FirstOrDefaultAsync(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            // 建立一個 ViewModel
            var model = new EditAccountViewModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.Name,
                DepartmentId = user.DepartmentId,
                RoleId = user.RoleId.ToString(),
                IsActive = user.IsActive,
                // 初始化下拉選單資料來源
                DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName", user.DepartmentId),
                RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName", user.RoleId)
            };

            return View(model);
        }

        [Authorize]//編輯帳號
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(EditAccountViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                // 再次初始化下拉選單資料
                model.DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName", model.DepartmentId);
                model.RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName", model.RoleId);
                return View(model);
            }

            // 從資料庫找到該使用者
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == model.UserID);
            if (user == null)
            {
                return NotFound();
            }

            // 更新可編輯的欄位，根據需求更新
            user.Email = model.Email;
            user.Name = model.FullName;
            user.RoleId = int.Parse(model.RoleId);
            user.DepartmentId = model.DepartmentId;
            user.IsActive = model.IsActive;
            user.UpdatedAt = DateTime.Now;

            // 若密碼有修改
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                // 依 DataAnnotations [Compare] 已自動比對，這裡再做一次保險檢查
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("NewPassword", "密碼和確認密碼不匹配。");
                    // 再初始化下拉選單資料
                    model.DepartmentList = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName", model.DepartmentId);
                    model.RoleList = new SelectList(_context.Roles.ToList(), "RoleId", "RoleName", model.RoleId);
                    return View(model);
                }
                // 使用 SimpleHash 進行加密後存入資料庫
                user.PasswordHash = SimpleHash.ComputeHash(model.NewPassword);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("AccountManagement", "Admin");
        }
    }
}
