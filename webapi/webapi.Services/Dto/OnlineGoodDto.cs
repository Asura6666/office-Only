using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class OnlineGoodDto
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public List<long> IdList { get; set; }
        ///// <summary>
        ///// 商品State(string 待上架/已上架/已下架)
        ///// </summary>
        //public string State { get; set; }
    }
}
