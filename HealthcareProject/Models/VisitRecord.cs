using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class VisitRecord
    {
        public string VisitReason { get; set; }
        public string Prescription { get; set; }
        public string Visitid { get; set; }
        public DateTime VisitDate { get; set; }
        public bool Visited { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
