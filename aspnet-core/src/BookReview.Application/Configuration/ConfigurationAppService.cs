using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BookReview.Configuration.Dto;

namespace BookReview.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BookReviewAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
