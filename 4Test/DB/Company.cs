using System;
using System.Collections.Generic;

namespace DB
{
    public partial class Company
    {
        public Company()
        {
            Works = new HashSet<Work>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
