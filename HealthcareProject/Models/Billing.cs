using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthcareProject.Models
{
    public partial class Billing
    {
        public Billing()
        {
            CardPayment = new HashSet<CardPayment>();
            CashPayment = new HashSet<CashPayment>();
            CheckPayment = new HashSet<CheckPayment>();
        }
        [DataType(DataType.DateTime)]
        public DateTime BillingDate { get; set; }
        public double BillingAmount { get; set; }
        public int BillingId { get; set; }
        public bool Paid { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual ICollection<CardPayment> CardPayment { get; set; }
        public virtual ICollection<CashPayment> CashPayment { get; set; }
        public virtual ICollection<CheckPayment> CheckPayment { get; set; }
    }
}
