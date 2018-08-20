using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Repository.Entity
{
    [Table("tags")]
    public class Tag
    {
        /// <summary>
        /// 标签Id
        /// </summary>
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 标签Name
        /// </summary>
        //[MaxLength(100), Required]
        public string Tag_name { get; set; }
        /// <summary>
        /// 标签删除状态(string  是/否)
        /// </summary>
        public string Is_deleted { get; set; }
        /// <summary>
        /// 标签创建时间
        /// </summary>
        public DateTime Created_at { get; set; }
        /// <summary>
        /// 标签更新时间
        /// </summary>
        public DateTime Updated_at { get; set; }
    }
}
