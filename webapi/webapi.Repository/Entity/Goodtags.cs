using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webapi.Repository.Entity
{
    [Table("goodtag")]
    public class Goodtags
    {
        public long Id { get; set; }
        public long Tag_id { get; set; }
        public long Good_id { get; set; }
    }
}
