using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookReview.Authorization;

namespace BookReview
{
    [DependsOn(
        typeof(BookReviewCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BookReviewApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BookReviewAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BookReviewApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
