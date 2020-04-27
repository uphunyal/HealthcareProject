using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class MedicalReport
    {
        public string MedicalreportName { get; set; }
        public string ReportFile { get; set; }
        public int MedicalreportId { get; set; }
        public int PatientId { get; set; }

        public virtual MedicalreportType Medicalreport { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
