using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class Appointment
    {
        public int ApptId { get; set; }
        public DateTime ApptDate { get; set; }
        public string AppointmentReason { get; set; }
        public string PatientEmail { get; set; }
        public int? PatientId { get; set; }
        public bool VisitRecord { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
