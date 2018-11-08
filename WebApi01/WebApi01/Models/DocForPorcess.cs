using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi01.Models
{
    public class DocForPorcess
    {
        public Guid DocId {
            get { return Guid.NewGuid(); }
            set { }
        }
        public string DocName { get; set; }
        public int UserId { get; set; }

        public DateTime OrderTime { get { return DateTime.Now; } set { } }

        public bool Processed { get; set; }
    }
}
