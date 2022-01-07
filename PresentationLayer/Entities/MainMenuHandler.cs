using DataLayer.Enums;
using DomainLayer.Queries;

namespace PresentationLayer.Entities
{
    public static class MainMenuHandler
    {
        public static bool Register()
        {
            string username = null, password = null;
            var userQuery = new UserQueries();

            Printer.PrintTitle("REGISTRACIJA");
            
            Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
            Checkers.CheckString(Console.ReadLine().Trim(), out string possibleName);
            if (possibleName.Count() is 0) return false;
            username = possibleName;

            Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
            Checkers.CheckString(Console.ReadLine().Trim(), out string possiblePass);
            if (possiblePass.Count() is 0) return false;
            password = possiblePass;

            if (userQuery.UserExists(username))
            {
                Printer.ConfirmMessageAndClear("Korisnik sa tim imenom vec postoji", MessageType.Error);
                return true;
            }
            
            userQuery.Register(username, password);
            Printer.ConfirmMessageAndClear($"Uspjesno ste registrirani kao {username}", MessageType.Success);

            TaskManager.Tasker(MenuManager.DashboardSwitcher);
            return false;
        }

        public static bool Login()
        {
            string username, password;
            bool loginSuccess;
            UserQueries queries = new UserQueries();

            Printer.PrintTitle("LOGIN");

            Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
            Checkers.CheckString(Console.ReadLine().Trim(), out string possibleName);
            if (possibleName.Count() is 0) return false;
            username = possibleName;

            Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
            Checkers.CheckString(Console.ReadLine().Trim(), out string possiblePass);
            if (possiblePass.Count() is 0) return false;
            password = possiblePass;

            loginSuccess = queries.Login(username, password);

            if (loginSuccess is true)
            {
                Printer.ConfirmMessageAndClear($"Uspjesno ste prijavljeni kao {username}", MessageType.Success);
                TaskManager.Tasker(MenuManager.DashboardSwitcher);
                return false;
            }

            Printer.ConfirmMessageAndClear($"Korisnik s imenom {username} i lozinkom {password} ne postoji u sustavu ili je deaktiviran", MessageType.Error);
            return true;
        }
    }
}
