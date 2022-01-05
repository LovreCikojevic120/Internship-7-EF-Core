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
                Printer.ConfirmMessage("Unos opcije izbornika neispravan");
                return true;
            }

            switch (result)
            {
                case (int)Enums.MainMenuOption.Register:
                    MainMenuHandler.Register();
                    break;
                case (int)Enums.MainMenuOption.Login:
                    MainMenuHandler.Login();
                    break;
                case (int)Enums.MainMenuOption.Exit:
                    Printer.ConfirmMessage("Izasli ste iz aplikacije");
                    return false;
                default:
                    Printer.ConfirmMessage("Neispravan unos");
                    break;
            }
            return true;
        }

        public static void DashboardSwitcher()
        {
            bool isValidInput;

            do
            {
                Printer.PrintDashboard();

                isValidInput = Checkers.CheckForNumber(Console.ReadLine(), out int result);

                if (isValidInput)
                {
                    switch (result)
                    {
                        case (int)Enums.DashboardOptions.Resources:
                            DashboardHandler.PrintEntitiesByTag();
                            break;
                        case (int)Enums.DashboardOptions.Users:
                            DashboardHandler.GetAllUsers();
                            break;
                        case (int)Enums.DashboardOptions.NoReplys:
                            DashboardHandler.GetNoReplyEntities();
                            break;
                        case (int)Enums.DashboardOptions.Popular:
                            DashboardHandler.GetPopularEntities();
                            break;
                        case (int)Enums.DashboardOptions.MyProfile:
                            DashboardHandler.GetUserInfo();
                            break;
                        case (int)Enums.DashboardOptions.Logout:
                            Printer.ConfirmMessage("Odjavljeni ste iz sustava");
                            return;
                        default:
                            Printer.ConfirmMessage("Unijeli ste ne postojecu opciju");
                            isValidInput = false;
                            break;
                    }
                }

            } while (isValidInput);
        }
    }
}
