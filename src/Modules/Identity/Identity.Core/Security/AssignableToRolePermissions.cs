using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Core.Security
{
    public static class AssignableToRolePermissions
    {
        #region Fields

        private static Lazy<IEnumerable<PermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<PermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lazy<IEnumerable<string>> _permissionNamesLazy = new Lazy<IEnumerable<string>>(
            GetPermisionNames, LazyThreadSafetyMode.ExecutionAndPublication);
        #endregion
        #region permissionNames
        public const string CanManageRole = "CanManageRole";
        #endregion //permissions


        #region Categories

        public const string CanManageCategory = "CanManageCategory";
        public const string CanCreateCategory = "CanCreateCategory";
        public const string CanEditCategory = "CanEditCategory";
        public const string CanViewCategory = "CanViewCategory";
        public const string CanDeleteCategroy = "CanDeleteCategory";
        #endregion


        #region Permissions
        public static readonly PermissionModel CanManagePanelPermission = new PermissionModel { Name = CanManageRole, Category = CanViewCategory, Description = "می تواند وارد پنل مدیریت نقش شود" };
        #endregion


        #region Properties
        public static IEnumerable<PermissionModel> Permissions { get { return _permissionsLazy.Value; } }

        public static IEnumerable<string> PermissionNames { get { return _permissionNamesLazy.Value; } }

        #endregion

        #region GetAllPermisions
        private static IEnumerable<PermissionModel> GetPermision()
        {
            return new List<PermissionModel>
            {
                CanManagePanelPermission,

            };
        }

        private static IEnumerable<string> GetPermisionNames()
        {
            return new List<string>()
            {
                CanManageRole,
            };
        }
        #endregion
        #region GetAsSelectedList
        public static IEnumerable<SelectListItem> GetAsSelectListItems()
        {
            return Permissions.Select(a => new SelectListItem { Text = a.Description, Value = a.Name }).ToList();
        }
        #endregion
    }
}
