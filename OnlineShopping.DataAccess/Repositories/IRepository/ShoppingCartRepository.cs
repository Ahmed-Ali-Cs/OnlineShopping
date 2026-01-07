using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Repositories.IRepository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbcontext db;
        public ShoppingCartRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }
        public void Update(ShoppingCart shoppingCart)
        {
            db.ShoppingCarts.Update(shoppingCart);
        }
    }
    
}
