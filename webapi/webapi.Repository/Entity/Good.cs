using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Repository.Entity
{
    [Table("goods")]
    public class Good
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 商品Number
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 商品Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 商品State(string 待上架/已上架/已下架)
        /// </summary>

        public string State { get; set; }
        /// <summary>
        /// 商品删除状态(string  是/否)
        /// </summary>

        public string Is_deleted { get; set; }
        /// <summary>
        /// 商品创建时间
        /// </summary>
        public DateTime Created_at { get; set; }
        /// <summary>
        /// 商品更新时间
        /// </summary>
        public DateTime Updated_at { get; set; }
    }
}
