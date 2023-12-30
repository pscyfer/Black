using Common.AspNetCore.DataTableConfig;

namespace Identity.ViewModels.UserManager;
public class UserManagerIndexVm
{

    public UserManagerIndexVm(int userActiveCount, int userNotActiveCount, DataTableModel dataTableUser, int allUserCount, int userNotConfirmAccount)
    {
        UserActiveCount = userActiveCount;
        UserNotActiveCount = userNotActiveCount;
        DataTableUser = dataTableUser;
        AllUserCount = allUserCount;
        UserNotConfirmAccount = userNotConfirmAccount;
    }
    public UserManagerIndexVm()
    {

    }
    public int UserActiveCount { get; set; }
    public int AllUserCount { get; set; }
    public int UserNotConfirmAccount { get; set; }
    public int UserNotActiveCount { get; set; }

    public DataTableModel DataTableUser { get; set; }
}
