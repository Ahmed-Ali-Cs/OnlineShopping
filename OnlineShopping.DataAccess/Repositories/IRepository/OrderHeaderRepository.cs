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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbcontext db;

        public OrderHeaderRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            db.OrderHeaders.Update(orderHeader);
        }
    }
}
