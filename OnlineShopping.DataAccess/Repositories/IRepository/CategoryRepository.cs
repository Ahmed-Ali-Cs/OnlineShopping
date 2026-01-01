using OnlineShopping.Data;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Repositories.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbcontext db;

        public CategoryRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }

        public void Update(Category category)
        {
            db.Categories.Update(category);
        }
    }
}
