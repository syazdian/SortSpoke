using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentProcessor.Models;

namespace WebApi01.Repository
{
    public class UserSeeds : IRepository
    {
        static IQueryable<Client> users = new List<Client>()
        {
            new Client{ClientId =1 ,  UserID = "u01", SLALevel = SLATiers.TenMinute},
            new Client{ClientId =2 ,  UserID = "u02", SLALevel = SLATiers.TenMinute},
            new Client{ClientId =3 ,  UserID = "u03", SLALevel = SLATiers.TenMinute},
            new Client{ClientId =4 ,  UserID = "u04", SLALevel = SLATiers.OneHour},
            new Client{ClientId =5 ,  UserID = "u05", SLALevel = SLATiers.OneHour},
            new Client{ClientId =6 ,  UserID = "u06", SLALevel = SLATiers.OneHour},
            new Client{ClientId =7 ,  UserID = "u07", SLALevel = SLATiers.BestEffort},
            new Client{ClientId =8 ,  UserID = "u08", SLALevel = SLATiers.BestEffort},
            new Client{ClientId =9 ,  UserID = "u09", SLALevel = SLATiers.BestEffort}

        }.AsQueryable();

        public IQueryable<Client> GetUsers()
        {
            return users;
        }

        public Client GetUserById(string userId)
        {
            return users.Where(x => x.UserID == userId).FirstOrDefault();
        }

    }
}
