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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbcontext db;

        public ApplicationUserRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            db.ApplicationUsers.Update(applicationUser);
        }
    }
}
