using Abp.Application.Services;
using BookReview.MultiTenancy.Dto;

namespace BookReview.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

