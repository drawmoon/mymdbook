using Furion.FriendlyException;

namespace WebApiApplication.Exceptions
{
    [ErrorCodeType]
    public enum ErrorCodes
    {
        [ErrorCodeItemMetadata("该用户“{0}”不存在")]
        UserNotFound = 100404,

        [ErrorCodeItemMetadata("该角色“{0}”不存在")]
        RoleNotFound = 200404,

        [ErrorCodeItemMetadata("权限不足")]
        PermissionDenied = 600403
    }
}
