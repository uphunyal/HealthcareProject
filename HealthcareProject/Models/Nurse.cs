using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class Nurse
    {
        public string NurseName { get; set; }
        public double Salary { get; set; }
        public string NurseEmail { get; set; }
        public int NurseId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
