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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbcontext db;

        public CompanyRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }

        public void Update(Company company)
        {
            db.Companies.Update(company);
        }
    }
}
