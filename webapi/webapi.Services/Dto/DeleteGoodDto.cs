using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class DeleteGoodDto
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public long Id { get; set; }
        

        /// <summary>
        /// 商品Number
        /// </summary>
        public string Number { get; set; }
    }
}
