﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.Publics.Dtos;
using CompanyName.ProjectName.Roles;
using CompanyName.ProjectName.Roles.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.Controllers.Systems
{
    [Route("Roles")]
    [Authorize(Policy = IdentityPermissions.Roles.Default)]
    public class RoleController : ProjectNameController
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }
        
        [HttpPost("all")]
        [SwaggerOperation(summary: "获取所有角色", Tags = new[] { "Roles" })]
        public Task<ListResultDto<IdentityRoleDto>> AllListAsync()
        {
            return _roleAppService.AllListAsync();
        }

        [HttpPost("page")]
        [SwaggerOperation(summary: "分页获取角色", Tags = new[] { "Roles" })]
        public Task<PagedResultDto<IdentityRoleDto>> ListAsync(PagingRoleListInput input)
        {
            return _roleAppService.ListAsync(input);
        }

        [HttpPost("create")]
        [Authorize(IdentityPermissions.Roles.Create)]
        [SwaggerOperation(summary: "创建角色", Tags = new[] { "Roles" })]
        public Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return _roleAppService.CreateAsync(input);
        }

        [HttpPost("update")]
        [Authorize(IdentityPermissions.Roles.Update)]
        [SwaggerOperation(summary: "更新角色", Tags = new[] { "Roles" })]
        public Task<IdentityRoleDto> UpdateAsync(UpdateRoleInput input)
        {
            return _roleAppService.UpdateAsync(input);
        }

        [HttpPost("delete")]
        [Authorize(IdentityPermissions.Roles.Delete)]
        [SwaggerOperation(summary: "删除角色", Tags = new[] { "Roles" })]
        public Task DeleteAsync(IdInput input)
        {
            return _roleAppService.DeleteAsync(input.Id);
        }


    }
}