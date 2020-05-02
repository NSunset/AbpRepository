using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Manage_PayDetail")]
    public class ManagePayDetail : FullAuditedEntity<long>
    {
        /// <summary>
        /// 款式Id
        /// </summary>
        public long StyleId { get; set; }


        /// <summary>
        /// Manage_PayHistory表主键Id
        /// </summary>
        public long ManagePayHistoryId { get; set; }


        /// <summary>
        /// 该订单对应的金额(含税)
        /// </summary>
        public decimal Amount { get; set; }

    }
}
