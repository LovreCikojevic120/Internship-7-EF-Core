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

                Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
                validName = Checkers.CheckString(Console.ReadLine().Trim(), out string possibleName);
                username = possibleName;

                Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
                validPassword = Checkers.CheckString(Console.ReadLine().Trim(), out string possiblePass);
                password = possiblePass;

                if (userQuery.UserExists(username))
                {
                    Console.WriteLine("Ime vec postoji!");
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

                Printer.ConfirmMessage($"Korisnik s imenom {username} i lozinkom {password} ne postoji u sustavu");

            } while (loginSuccess is false);
        }
    }
}
