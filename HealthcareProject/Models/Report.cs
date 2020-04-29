using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareProject.Models
{
    public class Report
    {
        public string DoctorEmail { get; set; }
        public DateTime Date { get; set; }

        public int Month { get; set; }
    }
}
