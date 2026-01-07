using OnlineShopping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Repositories.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbcontext db;

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }

        public UnitOfWork(ApplicationDbcontext db)
        {
            this.db = db;
            Category = new CategoryRepository(db);
            Product = new ProductRepository(db);
            Company = new CompanyRepository(db);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
