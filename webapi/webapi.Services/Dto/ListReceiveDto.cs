using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Dto
{
    public class ListReceiveDto
    {
        public string Name { get; set; }
        public string Tag { get; set; } = "";
        public string State { get; set; } = "";
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
