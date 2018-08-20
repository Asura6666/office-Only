using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class AlterGoodDto
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public long Id { get; set; }
        ///// <summary>
        ///// 商品Number
        ///// </summary>
        //public string Number { get; set; }
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
        /// 商品对应标签id
        /// </summary>
        public int[] TagsId { get; set; }
    }
}
