using DataLayer.Enums;

namespace PresentationLayer.Entities.Utility
{
    public class TaskManager
    {
        public delegate bool RunningTask();

        public delegate bool DashboardTask(ResourceTag tag);

        public delegate bool InteractTasker();

        public static void Tasker(RunningTask runningTask)
        {
            bool isRunning;

            do
            {
                isRunning = runningTask();

                if (isRunning is false) return;
            }
            while (isRunning);
        }

        public static void DashboardTasker(DashboardTask runningTask, ResourceTag tag)
        {
            bool isRunning;

            do
            {
                isRunning = runningTask(tag);

                if (isRunning is false) return;
            }
            while (isRunning);
        }

        public static void EntityInteracTask(InteractTasker interact)
        {
            
        }
    }
}
