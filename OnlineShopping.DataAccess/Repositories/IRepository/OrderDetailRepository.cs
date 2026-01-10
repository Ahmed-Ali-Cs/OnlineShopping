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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbcontext db;

        public OrderDetailRepository(ApplicationDbcontext db):base(db)
        {
            this.db = db;
        }

        public void Update(OrderDetail orderDetail)
        {
            db.OrderDetails.Update(orderDetail);
        }
    }
}
