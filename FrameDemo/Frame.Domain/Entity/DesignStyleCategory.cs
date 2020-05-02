using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Domain
{
    [Table("Design_StyleCategory")]
    public class DesignStyleCategory: Entity<long>
    {
        /// <summary>
        /// 款式Id
        /// </summary>
        public long StyleId { get; set; }

        /// <summary>
        /// 分类Id（program_category表 Id）
        /// </summary>
        public long CategoryId { get; set; }
    }
}
