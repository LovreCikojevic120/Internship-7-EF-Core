using DomainLayer.DatabaseEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Entities
{
    public class TaskManager
    {
        public delegate bool RunningTask();

        public delegate bool DashboardTask(ResourceTag tag);

        public static void Test(RunningTask runningTask)
        {
            bool isRunning;

            do
            {
                isRunning = runningTask();

                if (isRunning is false) return;
            }
            while (isRunning);
        }

        public static void Test2(DashboardTask runningTask, ResourceTag tag)
        {
            bool isRunning;

            do
            {
                isRunning = runningTask(tag);

                if (isRunning is false) return;
            }
            while (isRunning);
        }
    }
}
