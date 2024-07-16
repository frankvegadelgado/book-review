using Abp.Authorization;
using BookReview.Authorization.Roles;
using BookReview.Authorization.Users;

namespace BookReview.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
