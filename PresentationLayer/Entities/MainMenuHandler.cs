using DomainLayer.Entities;
using DomainLayer.Queries;

namespace PresentationLayer.Entities
{
    public static class MainMenuHandler
    {
        public static void Register()
        {
            string username, password;
            bool validName, validPassword;
            var userQuery = new UserQueries();

            do
            {
                Printer.PrintTitle("Registracija");

                Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
                validName = Checkers.CheckString(Console.ReadLine().Trim(), out string possibleName);
                if (possibleName.Count() is 0) return;
                username = possibleName;

                Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
                validPassword = Checkers.CheckString(Console.ReadLine().Trim(), out string possiblePass);
                if(possiblePass.Count() is 0) return;
                password = possiblePass;

                if (userQuery.UserExists(username))
                {
                    Printer.ConfirmMessage("Vec postoji");
                    validName = false;
                }

            } while (validName is false || validPassword is false);

            userQuery.Register(username, password);
            Printer.ConfirmMessage("Uspjesno ste registrirani");

            MenuManager.DashboardSwitcher();
        }

        public static void Login()
        {
            string username, password;
            bool loginSuccess;
            UserQueries queries = new UserQueries();

            do
            {
                Printer.PrintTitle("Login");

                Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
                username = Console.ReadLine().Trim();

                Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
                password = Console.ReadLine().Trim();

                loginSuccess = queries.Login(username, password);

                if (loginSuccess is true)
                {
                    Printer.ConfirmMessage($"Uspjesno ste prijavljeni kao: {DatabaseStateTracker.CurrentUser.UserName}");
                    MenuManager.DashboardSwitcher();
                    return;
                }

                Printer.ConfirmMessage($"Korisnik s imenom {username} i lozinkom {password} ne postoji u sustavu ili je deaktiviran");

            } while (loginSuccess is false);
        }
    }
}
