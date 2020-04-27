using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class DailyReport
    {
        public DateTime ReportDate { get; set; }
        public int NoPatients { get; set; }
        public string ReportId { get; set; }
        public string DoctorName { get; set; }
        public double DailyIncome { get; set; }
    }
}
