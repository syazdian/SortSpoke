using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentProcessor.Models
{
    public enum ProcessStatus
    {
        NotProcessed, InProcess, Complete
    };
    public class DocForPorcess:IDisposable
    {
        public Guid DocId {
            get { return Guid.NewGuid(); }
            set { }
        }
        public string DocName { get; set; }
        public SLATiers SLALevel { get; set; }

        public DateTime OrderTime { get; set; }
        public ProcessStatus Status { get; set; }

        public void Dispose()
        {
            this.Dispose();
        }
    }

    public class WaitingTimeDoc
    {
        public DocForPorcess Doc { get; set; }
        public double MinutesWaitingTime { get; set; }
    }
}
