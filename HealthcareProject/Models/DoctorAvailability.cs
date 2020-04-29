using System;
using System.Collections.Generic;

namespace HealthcareProject.Models
{
    public partial class DoctorAvailability
    {
        public int DoctorId { get; set; }
        public DateTime AvailableTime { get; set; }
        public int AvailabilityId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
