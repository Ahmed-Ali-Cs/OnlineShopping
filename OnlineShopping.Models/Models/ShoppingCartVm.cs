using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Models.Models
{
    public class ShoppingCartVm
    {
        public IEnumerable<ShoppingCart> ShoppingCartlist { get; set; }
        public double OrderTotal { get; set; }
    }
}
