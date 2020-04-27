using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
            DoctorUnavailability = new HashSet<DoctorUnavailability>();
            Nurse = new HashSet<Nurse>();
            VisitRecord = new HashSet<VisitRecord>();
        }

        public string DoctorName { get; set; }
        public double Salary { get; set; }
        public string DoctorEmail { get; set; }
        public int DoctorId { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<DoctorUnavailability> DoctorUnavailability { get; set; }
        public virtual ICollection<Nurse> Nurse { get; set; }
        public virtual ICollection<VisitRecord> VisitRecord { get; set; }
    }
}
