using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestParameters
{
    public class PaginationInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "Index";
        public string QueryString { get; set; } = "";
    }
}
