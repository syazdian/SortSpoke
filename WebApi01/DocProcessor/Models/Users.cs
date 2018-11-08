using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentProcessor.Models
{
   public enum SLATiers
    {
        TenMinute, OneHour, BestEffort
    };

    public class Client
    {
        public string UserID { get; set; }
        public int ClientId { get; set; }
        public  SLATiers SLALevel { get; set; }
    }
   

}
