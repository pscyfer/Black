namespace Identity.Core.Security
{
    public static class StandardRoles
    {
        #region Fields

        private static Lazy<IEnumerable<PermissionRecord>> _rolesWithPermissionsLazy =
            new Lazy<IEnumerable<PermissionRecord>>();
        private static Lazy<IEnumerable<string>> _rolesLazy = new Lazy<IEnumerable<string>>();
        #endregion

        #region Properties
        public static IEnumerable<string> SystemRoles
        {
            get
            {
                if (_rolesLazy.IsValueCreated)
                    return _rolesLazy.Value;
                _rolesLazy = new Lazy<IEnumerable<string>>(GetSystemRoles);
                return _rolesLazy.Value;
            }
        }

        public static IEnumerable<PermissionRecord> SystemRolesWithPermissions
        {
            get
            {
                if (_rolesWithPermissionsLazy.IsValueCreated)
                    return _rolesWithPermissionsLazy.Value;
                _rolesWithPermissionsLazy = new Lazy<IEnumerable<PermissionRecord>>(GetDefaultRolesWithPermissions);
                return _rolesWithPermissionsLazy.Value;
            }
        }
        #endregion

        #region DefaultRoles
        //TODO//: Add Standard Role Here
        public const string SuperAdmin = "مدیران";
        public const string Admin = "اپراتورها";
        public const string Basic = "مشتریان";

        #endregion

        #region GetSysmteRoles
        private static IEnumerable<string> GetSystemRoles()
        {
            return new List<string>
            {
              SuperAdmin,
              Admin,
              Basic
            };
        }
        #endregion

        #region GetDefaultRolesWithPermissions
        //TODO// دسترسی ها به رول ها باید درست شوند
        private static IEnumerable<PermissionRecord> GetDefaultRolesWithPermissions()
        {
            return new List<PermissionRecord>
            {
                new PermissionRecord
                {
                    RoleName = SuperAdmin,
                    Permissions = AssignableToRolePermissions.Permissions
                },
                new PermissionRecord
                {
                    RoleName = Admin,
                    Permissions = new List<PermissionModel>
                    {
                        AssignableToRolePermissions.CanManagePanelPermission,
                    }
                }
                ,new PermissionRecord
                {
                    RoleName=Basic,
                    Permissions=new List<PermissionModel>
                    {
                      AssignableToRolePermissions.CanManagePanelPermission,
                    }
                }
            };
        }
        #endregion

    }
}
