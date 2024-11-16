using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Domain
{
    public class Logs : BaseEntity
    {
        public string Type { get; set; }
        public DateTime TimestampUtc { get; set; }
        public string Message { get; set; }
    }
}
