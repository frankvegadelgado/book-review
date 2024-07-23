using Abp.Application.Services;
using Abp.Runtime.Caching;
using Microsoft.AspNetCore.Http;

namespace BookReview.Web.Host.Startup
{
    public abstract class BookReviewAppServiceBase : ApplicationService
    {

        protected BookReviewAppServiceBase()
        {
            LocalizationSourceName = BookReviewConsts.LocalizationSourceName;
        }
    }
}
