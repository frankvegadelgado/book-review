using System.Threading.Tasks;
using Abp.Application.Services;
using BookReview.Sessions.Dto;

namespace BookReview.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
