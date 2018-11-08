using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi01.Models
{
   public enum SLATiers
    {
        TenMinute, OneHour, BestEffort
    };
    public class Client
    {
        public string Username { get; set; }
        public int ClientId { get; set; }
        public  SLATiers AccessLevel { get; set; }
    }
   

}
