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

            do
            {
                Console.WriteLine("Upisi korisnicko ime (MIN 5 karaktera):");
                validName = Checkers.CheckString(Console.ReadLine().Trim(), out string possibleName);
                username = possibleName;

                Console.WriteLine("Upisite svoju lozinku (MIN 5 karaktera):");
                validPassword = Checkers.CheckString(Console.ReadLine().Trim(), out string possiblePass);
                password = possiblePass;

            } while (validName is false || validPassword is false);

            UserQueries queries = new UserQueries();
            queries.Register(username, password);

            MenuManager.DashboardMenu();
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
                    Console.WriteLine($"{DatabaseStateTracker.CurrentUser.UserName} " +
                        $"{DatabaseStateTracker.CurrentUser.Password}");
                    MenuManager.DashboardMenu();
                    return;
                }

                //no user
                Console.WriteLine("Nema!");

            } while (loginSuccess is false);
        }
    }
}
