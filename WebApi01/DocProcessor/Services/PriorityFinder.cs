using DocumentProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

namespace DocumentProcessor.Services
{
    public class PriorityFinder
    {
        public static DocForPorcess FindHighestPriorityForProcess(List<DocForPorcess> processQue)
        {
            WaitingTimeDoc tenMinSla
                = FindMaximumWaitedObj(processQue.Where(x => x.SLALevel == SLATiers.TenMinute).ToList());
            WaitingTimeDoc oneHourSla
               = FindMaximumWaitedObj(processQue.Where(x => x.SLALevel == SLATiers.OneHour).ToList());
            WaitingTimeDoc bestEfforSla
                = FindMaximumWaitedObj(processQue.Where(x => x.SLALevel == SLATiers.BestEffort).ToList());


            if (tenMinSla.MinutesWaitingTime >= 10)
                return tenMinSla.Doc;
            else if (oneHourSla.MinutesWaitingTime >= 60)
                return oneHourSla.Doc;
            else if (tenMinSla.Doc != null)
                return tenMinSla.Doc;
            else if (oneHourSla.Doc != null)
                return oneHourSla.Doc;
            else if (bestEfforSla.Doc != null)
                return bestEfforSla.Doc;

            return null;
        }

        public static WaitingTimeDoc FindMaximumWaitedObj(List<DocForPorcess> processQue)
        {
            double waitingTime = 0;
            WaitingTimeDoc timeDocObj = new WaitingTimeDoc();
            foreach (var item in processQue)
            {
                waitingTime = Abs((item.OrderTime-DateTime.Now).TotalMinutes);

                if (waitingTime > timeDocObj.MinutesWaitingTime)
                {
                    timeDocObj.MinutesWaitingTime = waitingTime;
                    timeDocObj.Doc = item;
                }
            }
            return timeDocObj;

        }
    }
}
