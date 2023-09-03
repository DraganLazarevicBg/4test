using System;
using System.Collections.Generic;

namespace DB
{
    public partial class Employee
    {
        public Employee()
        {
            Works = new HashSet<Work>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
