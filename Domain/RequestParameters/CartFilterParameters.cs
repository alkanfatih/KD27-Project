using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestParameters
{
    public class CartFilterParameters
    {
        public string? UserNameOrEmail { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public int? MinItemCount { get; set; }
    }
}
