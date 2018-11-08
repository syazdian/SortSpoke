using DocumentProcessor.Models;
using DocumentProcessor.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WebApi01.Helper;
using WebApi01.Repository;

namespace WebApi01.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DocProcessController : ControllerBase
    {
        private IRepository db;
        private readonly IOptions<ConfigurationSettings> confSettings;
        private List<DocForPorcess> processQue = Singleton.getQue;
        private int activeServersCount = Singleton.getServerCount;
        private int maxServerAllowed;
        private int processTime;
        private ILog logger;

        public DocProcessController(IRepository db, IOptions<ConfigurationSettings> confSettings)
        {
            this.db = db;
            this.confSettings = confSettings;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            logger = LogManager.GetLogger(typeof(Program));
        }



        [HttpGet]
        [Route("GetData")]
        public ActionResult<string> GetData(string userId, List<string> docNames)
        {
            if (userId == null || docNames.Count == 0) return "URL don't seem to be correct";
           /////
           ///Receiveng userID and List of DocNames and converting them to Client Object and a List of DocForProcess
           ///
            //Create Doc and UserObjects
            Client client = CreateObject.ClientObjMaker(userId, db);
            if (client == null) return "User Not Found!";
           
            logger.InfoFormat("Client {0}, with access level {1}, has added {2} tasks.", client.UserID, client.SLALevel, docNames.Count);

            List<DocForPorcess> taskObjList =  CreateObject.TaskListMaker(client, db, docNames);

            ///Collectin data from appseting.json
            maxServerAllowed = confSettings.Value.ServerNumber;
            processTime = confSettings.Value.ProcessTime;


            //All the new and remaining orders are in ProcessQue 
            processQue.AddRange(taskObjList);

            ///Creating list of tasks, with limitation of number of servers for simultaneous process.
            Scheduler.CreateThreads(maxServerAllowed, processQue, processTime, activeServersCount);

            return "Processing...";

        }






    }
}
