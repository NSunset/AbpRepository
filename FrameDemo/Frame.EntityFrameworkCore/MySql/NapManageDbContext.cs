using Abp.EntityFrameworkCore;
using Frame.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frame.Common;

namespace Frame.EntityFrameworkCore.MySql
{
    public class NapManageDbContext : AbpDbContext
    {

        public virtual DbSet<ManageUserInfo> ManageUserInfo { get; set; }

        public virtual DbSet<ManageUserRole> ManageUserRole { get; set; }

        public virtual DbSet<ManageRole> ManageRole { get; set; }

        public virtual DbSet<ManagePermission> ManagePermission { get; set; }

        public virtual DbSet<ManageRolePermission> ManageRolePermission { get; set; }

        public NapManageDbContext(DbContextOptions options) : base(options)
        {
        }

        public void Initialize()
        {
            var context = this;
            var flag = context.Database.EnsureCreated();
            if (flag) return;
            //初始权限
            var count = context.ManagePermission.IgnoreQueryFilters().Count();
            if (count < 1)
            {
                context.ManagePermission.Add(new ManagePermission { Id = 1, PerName = "款式审核", PerValue = "/index/myCreation", FatherId = 0, Type = PermissionType.MenuPermissions });
                context.ManagePermission.Add(new ManagePermission { Id = 2, PerName = "微信转账", PerValue = "/index/myMoney", FatherId = 0, Type = PermissionType.MenuPermissions });
                context.ManagePermission.Add(new ManagePermission { Id = 3, PerName = "转账统计", PerValue = "/index/redPackCount", FatherId = 0, Type = PermissionType.MenuPermissions });

                context.ManagePermission.Add(new ManagePermission { Id = 4, PerName = "查询款式列表", PerValue = "/DesignStyle/GetPageStyle", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 5, PerName = "查询款式详情", PerValue = "/DesignStyle/GetStyleDetail", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 6, PerName = "款式审核", PerValue = "/DesignStyle/Review", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 7, PerName = "款式下载", PerValue = "/DesignStyle/DownloadStyleImg", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 8, PerName = "款式统计", PerValue = "/DesignStyle/GetStyleStatistics", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 9, PerName = "获取设计师下拉框信息", PerValue = "/DesignUserInfo/GetDesignUserDowdlist", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 10, PerName = "获取设计师信息", PerValue = "/DesignUserInfo/GetDesignUser", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 11, PerName = "转账统计", PerValue = "/TransferStatistics/GetStyleStatistics", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 12, PerName = "获取转账记录数据分页", PerValue = "/TransferStatistics/GetTransferHistoryList", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 13, PerName = "获取转账详情", PerValue = "/TransferStatistics/GetTransferPayDetail", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 14, PerName = "导出发放红包数据到Excel", PerValue = "/ExcelExport/IssurRedPacketRxportToFile", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 15, PerName = "导出转账记录数据到Excel", PerValue = "/ExcelExport/TransferHistoryRxportToFile", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 16, PerName = "获取发放红包列表数据分页", PerValue = "/IssueRedPackets/GetIssurRedPacketList", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 17, PerName = "合并支付", PerValue = "/IssueRedPackets/CombinedPayment", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.ManagePermission.Add(new ManagePermission { Id = 18, PerName = "订单详情", PerValue = "/IssueRedPackets/GetStyleOrderDetail", FatherId = 0, Type = PermissionType.SystemAuthority });
                context.SaveChanges();
            }

            //初始角色
            var roleCount = context.ManageRole.IgnoreQueryFilters().Count();
            if (roleCount < 1)
            {
                var permissions = context.ManagePermission.IgnoreQueryFilters().ToList();
                //管理员
                context.ManageRole.Add(new ManageRole { Id = 1, RoleName = FrameCoreConsts.StaticRole_Admin, Sort = 1, FatherId = 0, IsEnable = true });

                //运营角色
                context.ManageRole.Add(new ManageRole { Id = 2, RoleName = FrameCoreConsts.StaticRole_Operation, Sort = 1, FatherId = 1, IsEnable = true });
                permissions.Where(a => new long[] { 1, 4, 5, 6, 7, 8, 9, 10 }.Contains(a.Id)).ToList().ForEach(a =>
                {
                    context.ManageRolePermission.Add(new ManageRolePermission { RoleId = 2, PermissionId = a.Id });
                });

                //财务角色
                context.ManageRole.Add(new ManageRole { Id = 3, RoleName = FrameCoreConsts.StaticRole_Finance, Sort = 1, FatherId = 1, IsEnable = true });
                permissions.Where(a => new long[] { 2, 3, 9, 11, 12, 13, 14, 15, 16, 17, 18 }.Contains(a.Id) || a.Id >= 11).ToList().ForEach(a =>
                {
                    context.ManageRolePermission.Add(new ManageRolePermission { RoleId = 3, PermissionId = a.Id });
                });
                context.SaveChanges();
            }


            //初始用户
            var user = context.ManageUserInfo.IgnoreQueryFilters().Count();
            if (user < 1)
            {
                //管理员
                context.ManageUserInfo.Add(new ManageUserInfo { Id = 1, Account = FrameCoreConsts.StaticUser_Admin, Passwd = EncryptDecode.GetMD5_32("qwer1234"), CreatorUserId = 0, CreationTime = DateTime.Now, IsDeleted = false });
                context.ManageUserRole.Add(new ManageUserRole { UserId = 1, RoleId = 1 });

                //运营人员
                //context.ManageUserInfo.Add(new ManageUserInfo { Id = 2, Account = FrameCoreConsts.StaticUser_Operation, Passwd = SecurityUtil.GetMD5_32("123456"), CreatorUserId = 1, CreationTime = DateTime.Now, IsDeleted = false });
                //context.ManageUserRole.Add(new ManageUserRole { UserId = 2, RoleId = 2 });

                //财务人员
                //context.ManageUserInfo.Add(new ManageUserInfo { Id = 3, Account = FrameCoreConsts.StaticUser_Finance, Passwd = SecurityUtil.GetMD5_32("123456"), CreatorUserId = 1, CreationTime = DateTime.Now, IsDeleted = false });
                //context.ManageUserRole.Add(new ManageUserRole { UserId = 3, RoleId = 3 });
                context.SaveChanges();
            }
        }
    }
}
