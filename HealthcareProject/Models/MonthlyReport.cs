using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class MonthlyReport
    {
        public DateTime ReportMonth { get; set; }
        public int NoPatients { get; set; }
        public string DoctorName { get; set; }
        public double MonthlyIncome { get; set; }
    }
}
