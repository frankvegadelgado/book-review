using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview
{
    public abstract class AsyncCrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>> where TEntity : class, IEntity<TPrimaryKey> where TEntityDto : IEntityDto<TPrimaryKey> where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected AsyncCrudAppServiceBase(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {
        }

        protected override string GetPermissionName { get => base.GetPermissionName; set => base.GetPermissionName = value; }
        protected override string GetAllPermissionName { get => base.GetAllPermissionName; set => base.GetAllPermissionName = value; }
        protected override string CreatePermissionName { get => base.CreatePermissionName; set => base.CreatePermissionName = value; }
        protected override string UpdatePermissionName { get => base.UpdatePermissionName; set => base.UpdatePermissionName = value; }
        protected override string DeletePermissionName { get => base.DeletePermissionName; set => base.DeletePermissionName = value; }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override Task<TEntityDto> CreateAsync(TCreateInput input)
        {
            return base.CreateAsync(input);
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]

        public override Task DeleteAsync(EntityDto<TPrimaryKey> input)
        {
            return base.DeleteAsync(input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override Task<PagedResultDto<TEntityDto>> GetAllAsync(TGetAllInput input)
        {
            return base.GetAllAsync(input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override Task<TEntityDto> GetAsync(EntityDto<TPrimaryKey> input)
        {
            return base.GetAsync(input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override string ToString()
        {
            return base.ToString();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public override Task<TEntityDto> UpdateAsync(TUpdateInput input)
        {
            return base.UpdateAsync(input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetAllInput input)
        {
            return base.ApplyPaging(query, input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetAllInput input)
        {
            return base.ApplySorting(query, input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void CheckCreatePermission()
        {
            base.CheckCreatePermission();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void CheckDeletePermission()
        {
            base.CheckDeletePermission();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void CheckGetAllPermission()
        {
            base.CheckGetAllPermission();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void CheckGetPermission()
        {
            base.CheckGetPermission();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void CheckPermission(string permissionName)
        {
            base.CheckPermission(permissionName);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void CheckUpdatePermission()
        {
            base.CheckUpdatePermission();
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override IQueryable<TEntity> CreateFilteredQuery(TGetAllInput input)
        {
            return base.CreateFilteredQuery(input);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override Task<TEntity> GetEntityByIdAsync(TPrimaryKey id)
        {
            return base.GetEntityByIdAsync(id);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override bool IsEnabled(string featureName)
        {
            return base.IsEnabled(featureName);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override Task<bool> IsEnabledAsync(string featureName)
        {
            return base.IsEnabledAsync(featureName);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override bool IsGranted(string permissionName)
        {
            return base.IsGranted(permissionName);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override Task<bool> IsGrantedAsync(string permissionName)
        {
            return base.IsGrantedAsync(permissionName);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override string L(string name)
        {
            return base.L(name);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override string L(string name, params object[] args)
        {
            return base.L(name, args);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override string L(string name, CultureInfo culture)
        {
            return base.L(name, culture);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override string L(string name, CultureInfo culture, params object[] args)
        {
            return base.L(name, culture, args);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override TEntity MapToEntity(TCreateInput createInput)
        {
            return base.MapToEntity(createInput);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override void MapToEntity(TUpdateInput updateInput, TEntity entity)
        {
            base.MapToEntity(updateInput, entity);
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        protected override TEntityDto MapToEntityDto(TEntity entity)
        {
            return base.MapToEntityDto(entity);
        }


    }

}
