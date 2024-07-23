using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview
{
    public class BookReviewServiceBase : AbpServiceBase
    {
        protected BookReviewServiceBase()
        {
            LocalizationSourceName = BookReviewConsts.LocalizationSourceName;
        }
    }
}
