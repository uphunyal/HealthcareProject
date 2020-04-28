﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthcareProject.Models
{
    public partial class DoctorUnavailability
    {
        [DataType(DataType.Date)]
        public DateTime Unavailability { get; set; }
        public int DoctorId { get; set; }
        [DataType(DataType.Time)]
        public DateTime UnavailableTime { get; set; }
        public int UnavailabilityId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
