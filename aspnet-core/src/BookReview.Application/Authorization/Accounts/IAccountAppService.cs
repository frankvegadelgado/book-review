using System.Threading.Tasks;
using Abp.Application.Services;
using BookReview.Authorization.Accounts.Dto;

namespace BookReview.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
