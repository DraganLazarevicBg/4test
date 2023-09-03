using System;
using System.Collections.Generic;

namespace DB
{
    public partial class Systemlog
    {
        public int Id { get; set; }
        public string ResourceType { get; set; } = null!;
        public int ResourceIdentifier { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Event { get; set; } = null!;
        public string Attributes { get; set; } = null!;
        public string? Comment { get; set; }
    }
}
