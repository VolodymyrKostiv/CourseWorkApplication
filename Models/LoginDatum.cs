using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class LoginDatum
    {
        public int EmployeeId { get; set; }
        public string LoginName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;
    }
}
