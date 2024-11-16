using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Domain
{
    public class Item : BaseEntity
    {
        public string Task { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime DueDateUtc { get; set; }
        public bool IsDone { get; set; }
    }
}
