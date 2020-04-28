using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthcareProject.Models
{
    public partial class DoctorAvailability
    {
        public int DoctorId { get; set; }
        [DataType(DataType.Time)]
        public DateTime AvailableTime { get; set; }
        public int AvailabilityId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
