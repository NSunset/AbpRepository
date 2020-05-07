using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("program_category")]
    public class ProgramCategory:Entity<long>
    {
        /// <summary>
        /// 方案分类名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 方案分类描述
        /// </summary>
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 方案分类排序
        /// </summary>
        public long Sort { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public long FatherId { get; set; }
    }
}
