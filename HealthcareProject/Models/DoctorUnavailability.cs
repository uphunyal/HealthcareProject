using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class DoctorUnavailability
    {
        public DateTime Unavailability { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
