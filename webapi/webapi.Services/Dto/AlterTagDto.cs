using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class AlterTagDto
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public long Tag_id { get; set; }

        /// <summary>
        /// 标签name
        /// </summary>
        public string Tag_name { get; set; }
    }
}
