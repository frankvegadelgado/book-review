using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BookReview.Controllers
{
    public abstract class BookReviewControllerBase: AbpController
    {
        protected BookReviewControllerBase()
        {
            LocalizationSourceName = BookReviewConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
