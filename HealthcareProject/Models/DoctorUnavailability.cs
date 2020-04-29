using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class DoctorUnavailability
    {
        public int DoctorId { get; set; }
        public DateTime Unavailable { get; set; }
        public int UnavailabilityId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
