using Abp.Application.Services;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Frame.Domain;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Frame.Common;
using System.Linq;
using Abp.BackgroundJobs;

namespace Frame.Application
{
    public interface ITestAppService : IApplicationService
    {
        Task<bool> ContainPermission(string url, string userName, string pwd);
    }

    public class TestAppService : FrameApplicationBase, ITestAppService
    {
        private readonly IRepository<ManageUserInfo, long> manageUserRepository;
        private readonly IRepository<ManagePermission, long> permissionRepository;
        private readonly IRepository<ManageUserRole, long> manageUrRepository;
        private readonly IRepository<ManageRolePermission, long> manageRolePRepository;
        private readonly IBackgroundJobManager backgroundJobManager;
        public TestAppService(IRepository<ManageUserInfo, long> manageUserRepository, IRepository<ManagePermission, long> permissionRepository, IRepository<ManageUserRole, long> manageUrRepository, IRepository<ManageRolePermission, long> manageRolePRepository,IBackgroundJobManager backgroundJobManager)
        {
            this.manageUserRepository = manageUserRepository;
            this.permissionRepository = permissionRepository;
            this.manageUrRepository = manageUrRepository;
            this.manageRolePRepository = manageRolePRepository;
            this.backgroundJobManager = backgroundJobManager;
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        //[Abp.Authorization.AbpAuthorize]
        [Abp.Authorization.AbpAllowAnonymous]
        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> Test()
        {
            var user = new ManageUserInfo { Id = 10010 };
            var token = await UserManage.GetToken(user);
            return token;
        }

        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> Get()
        {
            return "111";
        }

        [Abp.Authorization.AbpAllowAnonymous]
        [HttpPost]
        [SwaggerNotEnableAuth]
        public async Task<string> Login(string userName, string pwd)
        {
            var user = manageUserRepository.FirstOrDefault(a => a.Account == userName.Trim() && a.Passwd == Frame.Common.EncryptDecode.GetMD5_32(pwd.Trim()));
            if (user != null)
            {
                var token = await UserManage.GetToken(user);
                return token;
            }
            return "";
        }

        [CustomRoute(ApiVersions.v2, action: "ServerV2")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Abp.Authorization.AbpAllowAnonymous]
        public async Task<string> TestV2Ha()
        {
            backgroundJobManager.Enqueue<Frame.Domain.Sms.SimpleSendSmsJob, int>(new Random().Next(100, 1000));
            return "v2";
        }

        [HttpPost]
        public async Task<bool> AddDesignStyle()
        {
            var permission = new ManagePermission()
            {
                FatherId = 0,
                PerName = "巡山小队",
                PerValue = "小旋风",
                Type = 2
            };
            var id = permissionRepository.InsertAndGetId(permission);
            return id > 0;
        }

        [Abp.Authorization.AbpAllowAnonymous]
        [HttpPost]
        public async Task<bool> ContainPermission(string url, string userName, string pwd)
        {
            userName = userName.Trim();
            pwd = Frame.Common.EncryptDecode.GetMD5_32(pwd.Trim());
            var user = manageUserRepository.GetAllIncluding(a => a.ManageUserRoles).FirstOrDefault(a => a.Account == userName && a.Passwd == pwd);
            if (user != null)
            {
                var roleId = user.ManageUserRoles.Select(a => a.RoleId);
                var permissionId = manageRolePRepository.GetAll().Where(a => roleId.Contains(a.RoleId)).Select(a => a.PermissionId).ToList();
                if (permissionId != null && permissionId.Any())
                {
                    IEnumerable<ManagePermission> permissions = permissionRepository.GetAll().Where(a => permissionId.Contains(a.Id));
                    return permissions.Any(a => a.PerValue.ToLower() == url);
                }
                return true;
            }
            return false;
        }
        
    }
}
