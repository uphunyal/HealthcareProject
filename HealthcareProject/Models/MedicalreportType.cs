using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class MedicalreportType
    {
        public MedicalreportType()
        {
            MedicalReport = new HashSet<MedicalReport>();
        }

        public int MedicalreportId { get; set; }
        public string ReportType { get; set; }

        public virtual ICollection<MedicalReport> MedicalReport { get; set; }
    }
}
