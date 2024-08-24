using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Comment
    {
        public int? StockId { get; set; }
        // Navigation property
        public Stock? Stock { get; set; }
    }
}