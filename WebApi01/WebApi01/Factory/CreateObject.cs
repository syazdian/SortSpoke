using DocumentProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi01.Repository;

namespace WebApi01.Helper
{
     static class CreateObject
    {
         static CreateObject()
        {
        }
        public static Client ClientObjMaker(string userId, IRepository db)
        {
            Client client;
            return db.GetUserById(userId);
        }

        public static List<DocForPorcess> TaskListMaker(Client client, IRepository db, List<string> docNames)
        {
            List<DocForPorcess> taskObjList;

            if (client != null)
            {
                taskObjList = new List<DocForPorcess>();
                foreach (var item in docNames)
                {
                    DocForPorcess doc = new DocForPorcess();
                    doc.DocName = item;
                    doc.OrderTime = DateTime.Now;
                    doc.Status = ProcessStatus.NotProcessed;
                    doc.SLALevel = client.SLALevel;
                    taskObjList.Add(doc);
                }
                return taskObjList;
            }
         return null;
        }


    }
}
