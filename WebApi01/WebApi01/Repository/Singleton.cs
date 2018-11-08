using DocumentProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi01.Repository
{
    public  class Singleton
    {
        Singleton()
        {
        }

        private static List<DocForPorcess> processQue = null;
        private static  int? activeServersCount=null;
        //private static  int? threadCounter = null;
        private static readonly object padlock = new object();

     


            public static List<DocForPorcess> getQue
        {
                get
                {
                    if (processQue == null)
                    {
                    processQue = new List<DocForPorcess>();
                    }
                    return processQue;
                }
            }


        

        public static int getServerCount
        {
            get
            {
                lock (padlock)
                {
                    if (activeServersCount == null)
                    {
                        activeServersCount = 0;
                    }
                    return (int)activeServersCount;
                }
            }
        }


        //public static int getthreadCounter
        //{
        //    get
        //    {
        //        lock (padlock)
        //        {
        //            if (threadCounter == null)
        //            {
        //                threadCounter =0;
        //            }
        //            return (int)threadCounter;
        //        }
        //    }
        //}

        


    }
}
