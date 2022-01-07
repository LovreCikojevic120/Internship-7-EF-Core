using DataLayer.Enums;
using DomainLayer.Queries;

namespace PresentationLayer.Entities
{
    public static class UserInteract
    {
        public static bool StartInteract()
        {
            Printer.PrintUserInteractMenu();

            Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

            switch (menuOption)
            {
                case 1:
                    TemporaryDeactivate();
                    break;
                case 2:
                    PermaDeactivate();
                    break;
                case 3:
                    Reactivate();
                    break;
                case 4:
                    return true;
                default:
                    Printer.ConfirmMessageAndClear("Krivi unos!", MessageType.Error);
                    return false;
            }
            return false;
        }

        public static bool StartSettings()
        {
            Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

            switch (menuOption)
            {
                case 1:
                    EditUserName();
                    break;
                case 2:
                    EditPassword();
                    break;
                case 3:
                    Printer.ConfirmMessageAndClear("Vracate se na dashboard", MessageType.Note);
                    return true;
                default:
                    Printer.ConfirmMessageAndClear("Krivi unos!", MessageType.Error);
                    return false;
            }
            return false;
        }

        private static void EditPassword()
        {
            var userQuery = new UserQueries();
            Console.WriteLine("Novo ime?");
            var validInput = Checkers.CheckString(Console.ReadLine().Trim(), out string result);
            userQuery.EditPassword(result);
        }

        private static void EditUserName()
        {
            var userQuery = new UserQueries();
            Console.WriteLine("Novi pass?");
            var validInput = Checkers.CheckString(Console.ReadLine().Trim(), out string result);
            userQuery.EditUserName(result);
        }

        private static void Reactivate()
        {
            var userQuery = new UserQueries();

            Console.WriteLine("Id?");
            var userId = int.Parse(Console.ReadLine());

            userQuery.Reactivate(userId);
        }

        private static void PermaDeactivate()
        {
            var userQuery = new UserQueries();

            Console.WriteLine("Id?");
            var userId = int.Parse(Console.ReadLine());

            userQuery.PermaDeactivate(userId);
        }

        private static void TemporaryDeactivate()
        {
            var userQuery = new UserQueries();

            Console.WriteLine("Id?");
            var userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Dani?");
            var numberOfDays = int.Parse(Console.ReadLine());

            userQuery.TemporaryDeactivate(userId, numberOfDays);
        }
    }
}
