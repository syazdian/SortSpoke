using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentProcessor.Models;

namespace WebApi01.Repository
{
   public interface IRepository
    {
         IQueryable<Client> GetUsers();
         Client GetUserById(string userId);

    }
}
