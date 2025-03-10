using personal_tasks.Models;

namespace personal_tasks.ViewModels
{
    public class AccountManagementViewModel
    {
        public IEnumerable<Users> Users { get; set; } = Enumerable.Empty<Users>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        
        // 儲存篩選條件：
        public int? DepartmentFilter { get; set; }
        public int? RoleFilter { get; set; }
    }
}
