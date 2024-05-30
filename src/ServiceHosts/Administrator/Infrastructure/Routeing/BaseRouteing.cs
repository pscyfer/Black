namespace Administrator.Infrastructure.Routeing
{
    public static class BaseRouteing
    {
        private const string RouteDefault = "[controller]/[action]";

        #region UserManagmentRouteing

        public const string RouteDefaultUserId = "[controller]/[action]/{userId}";

        #endregion
        #region DynamicAccessManagementRouteing

        public const string DynamicAccessManagementRoleId = "[controller]/[action]/{Identifier}";

        #endregion
        #region RoleManagmentRouteing
        public const string RouteDefaultRoleId = "[controller]/[action]/{roleId}";
        public const string RouteDefaultOptinalRoleId = "[controller]/[action]/{roleId?}";
        #endregion

        #region TicketManagmentRouting

        public const string RouteDefaultTicketId = "[controller]/[action]/{id}";

        #endregion

        #region MonitorManagerRouteing
        public const string RouteDefaultMonitorId = "[controller]/[action]/{id}";
        public const string RouteDefaultMonitorIdentifier = "[controller]/[action]/{Identifier}";
        public const string RouteChangeIsPause = "[controller]/[action]/{id}/{state}";
        #endregion
    }
}
