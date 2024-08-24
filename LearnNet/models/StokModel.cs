using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Stok
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.empty;
        public string CompanyName { get; set; } = string.empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.empty;
        public long MyProperty { get; set; }

        public List<Comment> Comments { get; set; } = new list<Comment> ();
    }
}