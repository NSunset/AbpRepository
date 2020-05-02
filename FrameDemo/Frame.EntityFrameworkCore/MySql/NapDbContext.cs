using Abp.EntityFrameworkCore;
using Frame.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.EntityFrameworkCore.MySql
{
    public class NapDbContext : AbpDbContext
    {
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        public virtual DbSet<ProgramCategory> ProgramCategory { get; set; }

        public NapDbContext(DbContextOptions options) : base(options)
        {
        }


        public static void Initialize(NapDbContext context)
        {
            var flage = context.Database.EnsureCreated();
            if (flage) return;

        }
    }
}
