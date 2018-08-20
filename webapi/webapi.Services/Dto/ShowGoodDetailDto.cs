using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class ShowGoodDetailDto
    {
        /// <summary>
        /// 商品id
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
        /// 商品创建时间
        /// </summary>
        public DateTime Created_at { get; set; }
        /// <summary>
        /// 商品更新时间
        /// </summary>
        public DateTime Updated_at { get; set; }

        public List<string> TagsNameList { get; set; }
    }
}
