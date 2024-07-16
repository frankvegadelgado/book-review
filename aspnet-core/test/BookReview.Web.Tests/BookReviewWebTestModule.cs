using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookReview.EntityFrameworkCore;
using BookReview.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BookReview.Web.Tests
{
    [DependsOn(
        typeof(BookReviewWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BookReviewWebTestModule : AbpModule
    {
        public BookReviewWebTestModule(BookReviewEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BookReviewWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BookReviewWebMvcModule).Assembly);
        }
    }
}