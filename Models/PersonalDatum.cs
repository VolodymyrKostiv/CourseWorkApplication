using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class PersonalDatum
    {
        public int EmployeeId { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;
    }
}
