using DomainLayer.Entities;
using DomainLayer.Queries;

namespace PresentationLayer.Entities
{
    public static class MainMenuHandler
    {
        public static bool Register()
        {
            string username, password;
            bool validName, validPassword;
            var userQuery = new UserQueries();

            Printer.PrintTitle("Registracija");

            Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
            validName = Checkers.CheckString(Console.ReadLine().Trim(), out string possibleName);
            if (possibleName.Count() is 0) return false;
            username = possibleName;

            Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
            validPassword = Checkers.CheckString(Console.ReadLine().Trim(), out string possiblePass);
            if (possiblePass.Count() is 0) return false;
            password = possiblePass;

            if (userQuery.UserExists(username) || !validName || !validPassword)
            {
                Printer.ConfirmMessageAndClear("Vec postoji");
                return true;
            }
            
            userQuery.Register(username, password);
            Printer.ConfirmMessageAndClear("Uspjesno ste registrirani");

            TaskManager.Test(MenuManager.DashboardSwitcher);
            return false;
        }

        public static bool Login()
        {
            string username, password;
            bool loginSuccess;
            UserQueries queries = new UserQueries();

            Printer.PrintTitle("Login");

            Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
            username = Console.ReadLine().Trim();
            if (username.Count() is 0) return false;

            Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
            password = Console.ReadLine().Trim();
            if (password.Count() is 0) return false;

            loginSuccess = queries.Login(username, password);

            if (loginSuccess is true)
            {
                Printer.ConfirmMessageAndClear($"Uspjesno ste prijavljeni kao: {DatabaseStateTracker.CurrentUser.UserName}");
                TaskManager.Test(MenuManager.DashboardSwitcher);
                return false;
            }

            Printer.ConfirmMessageAndClear($"Korisnik s imenom {username} i lozinkom {password} ne postoji u sustavu ili je deaktiviran");
            return true;
        }
    }
}
