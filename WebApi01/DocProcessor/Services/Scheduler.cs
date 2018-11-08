using DocumentProcessor.Models;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;


namespace DocumentProcessor.Services
{
    public  class Scheduler : IDisposable
    {
        private  readonly ILog logger;
       
        public Scheduler()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            //logger = LogManager.GetLogger(typeof(Program));
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
         }


        ///Creating list of tasks, with limitation of number of servers for simultaneous process.
        public static async void CreateThreads( int maxServerAllowed, List<DocForPorcess> processQue, int processTime, int activeServersCount)
        {
            ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            List<Task> tasks;
            int allTasksCount = processQue.Count();
            for (int i = 0; i < allTasksCount + 1;)
            {
                tasks = new List<Task>();
                for (int j = 0; j < maxServerAllowed && i <= allTasksCount + 1; j++, i++)
                {
                    DocForPorcess doc = PriorityFinder.FindHighestPriorityForProcess(processQue);
                    if (doc == null) return;
                    processQue.Remove(doc);
                    Task task = Task.Run(() => Scheduler.CallProcess(doc, processTime));
                    tasks.Add(task);
                    activeServersCount++;
                }
                if (activeServersCount >= maxServerAllowed)
                {
                    Thread.Sleep(processTime + 100);
                    logger.InfoFormat("Required servers are {0} and we have {1} so we have to wait for releasing servers. ", activeServersCount, maxServerAllowed);
                }

                logger.InfoFormat("Items remaining in que are {0} and count of active CPUs is {1} ", processQue.Count, activeServersCount);
                await Task.WhenAll(tasks.ToArray());
                activeServersCount = 0;

                tasks.Clear();
            }

        }




        public static void CallProcess(DocForPorcess doc, int processTime)
        {
            ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            logger.InfoFormat("Start working on {0}  ", doc.DocName);
            doc.Status = ProcessStatus.InProcess;

            DoProcess(doc, processTime);
            doc.Status = ProcessStatus.Complete;
            logger.InfoFormat("Finish working on {0}  ", doc.DocName);

        }



        public static void DoProcess(DocForPorcess doc, int processTime)
        {
            Thread.Sleep(processTime);
            doc.Status = ProcessStatus.Complete;
         
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }


}
