using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utils
{
    public class StockObjectQuery
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

        public int PageSize { get; set; } = 10;

        public int PageNumber {get; set; } = 1;    
    }
}
