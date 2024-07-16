using System.Threading.Tasks;
using BookReview.Configuration.Dto;

namespace BookReview.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
