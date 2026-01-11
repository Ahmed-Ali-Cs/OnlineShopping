using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Utilities
{
    public class SD
    {
        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";
        public const string ManagerRole = "Manager";
        public const string CompanyRole = "Company";


        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string PaymentStatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayed Payment";
        public const string PaymentStatusRejected = "Rejected";
    }
}
