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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbcontext db;

        public ProductRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }

        public void Update(Product product)
        {
            db.Products.Update(product);
        }
    }
}
