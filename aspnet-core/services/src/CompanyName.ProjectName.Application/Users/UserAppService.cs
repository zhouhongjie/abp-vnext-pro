﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.QueryManagement.Systems.Users;
using CompanyName.ProjectName.Users.Dtos;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace CompanyName.ProjectName.Users
{
    public class UserAppService : ProjectNameAppService, IUserAppService
    {
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IdentityUserManager _userManager;
        private readonly IIdentityUserRepository _identityUserRepository;
       
        public UserAppService(
            IIdentityUserAppService identityUserAppService,
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository 
            )
        {
            _identityUserAppService = identityUserAppService;
            _userManager = userManager;
            _identityUserRepository = userRepository;
        }

        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<IdentityUserDto>> ListAsync(PagingUserListInput input)
        {
            var request = new GetIdentityUsersInput
            {
                Filter = input.Filter?.Trim(),
                MaxResultCount = input.PageSize,
                SkipCount = input.SkipCount,
                Sorting = " LastModificationTime desc"
            };
     
            long count = await _identityUserRepository.GetCountAsync(request.Filter)
                .ConfigureAwait(continueOnCapturedContext: false);
            List<Volo.Abp.Identity.IdentityUser> source = await _identityUserRepository
                .GetListAsync(request.Sorting, request.MaxResultCount, request.SkipCount, request.Filter)
                .ConfigureAwait(continueOnCapturedContext: false);

            return new PagedResultDto<IdentityUserDto>(count,
                base.ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<IdentityUserDto>>(source));
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            return await _identityUserAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUserDto> UpdateAsync(UpdateUserInput input)
        {
            return await _identityUserAppService.UpdateAsync(input.UserId, input.UserInfo);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task DeleteAsync(Guid id)
        {
            await _identityUserAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ListResultDto<IdentityRoleDto>> GetRoleByUserId(Guid userId)
        {
            return await _identityUserAppService.GetRolesAsync(userId);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(ChangePasswordInput input)
        {
            var identityUser = await _userManager.GetByIdAsync(base.CurrentUser.GetId());
            IdentityResult result;
            if (identityUser.PasswordHash == null)
            {
                result = await _userManager.AddPasswordAsync(identityUser, input.NewPassword);
            }
            else
            {
                result = await _userManager.ChangePasswordAsync(identityUser, input.CurrentPassword, input.NewPassword);
            }

            return !result.Succeeded
                ? throw new UserFriendlyException(result?.Errors?.FirstOrDefault()?.Description)
                : result.Succeeded;
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task LockAsync(LockUserInput input)
        {
            var identityUser = await _userManager.GetByIdAsync(input.UserId);
            await _userManager.SetLockoutEnabledAsync(identityUser, input.Locked);
            if (input.Locked)
            {
                // 如果锁定用户，锁定100年
                await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.UtcNow.AddYears(100));
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.UtcNow.AddDays(-1));
            }
        }
    }
}