﻿using PresentationLayer.Enums;
using DomainLayer.Queries;
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
                    //Exit
                    return false;
                default:
                    Console.WriteLine("Neispravan unos!");
                    break;
            }
            return true;
        }

        public static void DashboardMenu()
        {
            var isLogout = false;
            do
            {
                isLogout = DashboardSwitcher();

            } while (isLogout);
        }

        private static bool DashboardSwitcher()
        {
            Printer.PrintDashboard();

            var isValidInput = Checkers.CheckForNumber(Console.ReadLine(), out int result);

            if (!isValidInput)
            {
                Printer.ConfirmMessage("Unos opcije izbornika neispravan");
                return true;
            }

            switch (result)
            {
                case (int)Enums.DashboardOptions.Resources:
                    DashboardHandler.GetResourcesByTag();
                    break;
                case (int)Enums.DashboardOptions.Users:
                    DashboardHandler.GetAllUsers();
                    break;
                case (int)Enums.DashboardOptions.NoReplys:
                    DashboardHandler.GetNoReplyResouces();
                    break;
                case (int)Enums.DashboardOptions.Popular:
                    DashboardHandler.GetPopularResources();
                    break;
                case (int)Enums.DashboardOptions.MyProfile:
                    DashboardHandler.GetUserInfo();
                    break;
                case (int)Enums.DashboardOptions.Logout:
                    Printer.ConfirmMessage("Odjavljeni ste iz sustava");
                    return false;
                default:
                    Console.WriteLine("Neispravan unos!");
                    break;
            }
            return true;
        }
    }
}
