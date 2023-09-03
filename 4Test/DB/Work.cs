using System;
using System.Collections.Generic;

namespace DB
{
    public partial class Work
    {
        public int Id { get; set; }
        public int Comany { get; set; }
        public int Employee { get; set; }

        public virtual Company ComanyNavigation { get; set; } = null!;
        public virtual Employee EmployeeNavigation { get; set; } = null!;
    }
}
