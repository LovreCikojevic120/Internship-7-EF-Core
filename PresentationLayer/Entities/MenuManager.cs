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
                Printer.ConfirmMessageAndClear("Unos opcije izbornika neispravan");
                return true;
            }

            switch (result)
            {
                case (int)Enums.MainMenuOption.Register:
                    TaskManager.Test(MainMenuHandler.Register);
                    break;
                case (int)Enums.MainMenuOption.Login:
                    TaskManager.Test(MainMenuHandler.Login);
                    break;
                case (int)Enums.MainMenuOption.Exit:
                    Printer.ConfirmMessageAndClear("Izasli ste iz aplikacije");
                    return false;
                default:
                    Printer.ConfirmMessageAndClear("Neispravan unos");
                    return true;
            }
            return true;
        }

        public static bool DashboardSwitcher()
        {
            bool isValidInput;
            var dashboardHandler = new DashboardHandler();

            Printer.PrintDashboard();

            isValidInput = Checkers.CheckForNumber(Console.ReadLine(), out int result);

            if (isValidInput)
            {
                switch (result)
                {
                    case (int)Enums.DashboardOptions.Resources:
                        TaskManager.Test(dashboardHandler.ResourceTagSelect);
                        TaskManager.Test2(dashboardHandler.PrintEntitiesByTag, DatabaseStateTracker.currentResourceTag);
                        break;
                    case (int)Enums.DashboardOptions.Users:
                        TaskManager.Test(dashboardHandler.GetAllUsers);
                        break;
                    case (int)Enums.DashboardOptions.NoReplys:
                        TaskManager.Test(dashboardHandler.ResourceTagSelect);
                        TaskManager.Test2(dashboardHandler.GetNoReplyEntities, DatabaseStateTracker.currentResourceTag);
                        break;
                    case (int)Enums.DashboardOptions.Popular:
                        TaskManager.Test(dashboardHandler.GetPopularEntities);
                        break;
                    case (int)Enums.DashboardOptions.MyProfile:
                        TaskManager.Test(dashboardHandler.GetUserInfo);
                        break;
                    case (int)Enums.DashboardOptions.Logout:
                        Printer.ConfirmMessageAndClear("Odjavljeni ste iz sustava");
                        return false;
                    default:
                        Printer.ConfirmMessageAndClear("Unijeli ste ne postojecu opciju");
                        return true;
                }
            }
            return true;
        }
    }
}
