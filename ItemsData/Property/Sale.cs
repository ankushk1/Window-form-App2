using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGroupBy
{
     public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate{get; set;}
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}
