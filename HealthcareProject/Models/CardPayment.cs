using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class CardPayment
    {
        public string ReferenceNo { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BillingId { get; set; }

        public virtual Billing Billing { get; set; }
    }
}
