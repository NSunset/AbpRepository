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
        private readonly IRepository<ManageUserRole, long> manageUserRoleRepository;
        private readonly IRepository<ManageRolePermission, long> manageRolePRepository;
        private readonly IBackgroundJobManager backgroundJobManager;
        public TestAppService(IRepository<ManageUserInfo, long> manageUserRepository, IRepository<ManagePermission, long> permissionRepository, IRepository<ManageUserRole, long> manageUrRepository, IRepository<ManageRolePermission, long> manageRolePRepository, IBackgroundJobManager backgroundJobManager,
            IRepository<ManageUserRole, long> manageUserRoleRepository)
        {
            this.manageUserRepository = manageUserRepository;
            this.permissionRepository = permissionRepository;
            this.manageUrRepository = manageUrRepository;
            this.manageRolePRepository = manageRolePRepository;
            this.backgroundJobManager = backgroundJobManager;
            this.manageUserRoleRepository = manageUserRoleRepository;
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
            backgroundJobManager.Enqueue<Frame.BackgroundWorker.SimpleSendSmsJob, int>(new Random().Next(100, 1000));
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
                Type = PermissionType.SystemAuthority
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
            var query = (from user in manageUserRepository.GetAll()
                         join ur in manageUserRoleRepository.GetAll() on user.Id equals ur.UserId
                         join rp in manageRolePRepository.GetAll() on ur.RoleId equals rp.RoleId into urps
                         from urp in urps.DefaultIfEmpty()
                         where user.Account == userName && user.Passwd == pwd
                         select new
                         {
                             user.Id,
                             urp.PermissionId,
                         }).ToList();

            if (query != null && query.First().Id > 0)
            {
                var permissionId = query.Select(a => a.PermissionId).Distinct().ToList();
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
