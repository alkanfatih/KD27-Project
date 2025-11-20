using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int? CategoryId { get; set; }
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
        public bool IsValidPrice => MaxPrice > MinPrice;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortOrder { get; set; }

        public ProductRequestParameters() : this(1, 9)
        {

        }
        public ProductRequestParameters(int pageNumber = 1, int pageSize = 9)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
