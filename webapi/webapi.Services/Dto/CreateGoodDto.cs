using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class CreateGoodDto
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
        ///// <summary>
        ///// 商品删除
        ///// </summary>
        //public string Is_Deleted { get; set; }
        /// <summary>
        /// 商品对应标签id
        /// </summary>
        public int[] TagsId { get; set; }
    }
}
