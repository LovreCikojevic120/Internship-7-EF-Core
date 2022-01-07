using DataLayer.Enums;
using DomainLayer.Entities;

namespace PresentationLayer.Entities
{
    public class MenuManager
    {
        

        public static bool MainMenuSwitcher()
        {
            Printer.PrintMainMenu();

            var isValidInput = Checkers.CheckForNumber(Console.ReadLine(), out int result);

            if (!isValidInput)
            {
                Printer.ConfirmMessageAndClear("Unos opcije izbornika neispravan", MessageType.Error);
                return true;
            }

            switch (result)
            {
                case (int)MainMenuOption.Register:
                    TaskManager.Tasker(MainMenuHandler.Register);
                    break;
                case (int)MainMenuOption.Login:
                    TaskManager.Tasker(MainMenuHandler.Login);
                    break;
                case (int)MainMenuOption.Exit:
                    Printer.ConfirmMessageAndClear("Izasli ste iz aplikacije", MessageType.Note);
                    return false;
                default:
                    Printer.ConfirmMessageAndClear("Neispravan unos", MessageType.Error);
                    return true;
            }
            return true;
        }

        public static bool DashboardSwitcher()
        {
            bool isValidInput;
            var dashboardHandler = new DashboardHandler();

            Printer.PrintTitle("DASHBOARD");
            Printer.PrintDashboard();

            isValidInput = Checkers.CheckForNumber(Console.ReadLine(), out int result);

            if (isValidInput)
            {
                switch (result)
                {
                    case (int)DashboardOptions.Resources:
                        TaskManager.Tasker(dashboardHandler.ResourceTagSelect);
                        TaskManager.DashboardTasker(dashboardHandler.PrintEntitiesByTag, DatabaseStateTracker.currentResourceTag);
                        break;
                    case (int)DashboardOptions.Users:
                        TaskManager.Tasker(dashboardHandler.GetAllUsers);
                        break;
                    case (int)DashboardOptions.NoReplys:
                        TaskManager.Tasker(dashboardHandler.ResourceTagSelect);
                        TaskManager.DashboardTasker(dashboardHandler.GetNoReplyEntities, DatabaseStateTracker.currentResourceTag);
                        break;
                    case (int)DashboardOptions.Popular:
                        TaskManager.Tasker(dashboardHandler.GetPopularEntities);
                        break;
                    case (int)DashboardOptions.MyProfile:
                        TaskManager.Tasker(dashboardHandler.GetUserInfo);
                        break;
                    case (int)DashboardOptions.Logout:
                        Printer.ConfirmMessageAndClear("Odjavljeni ste iz sustava", MessageType.Note);
                        return false;
                    default:
                        Printer.ConfirmMessageAndClear("Unijeli ste ne postojecu opciju", MessageType.Error);
                        return true;
                }
            }
            return true;
        }
    }
}
