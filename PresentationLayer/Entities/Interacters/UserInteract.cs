using DataLayer.Enums;
using DomainLayer.Queries;
using PresentationLayer.Entities.Utility;

namespace PresentationLayer.Entities.Interacters
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
                    Printer.ConfirmMessageAndClear("Vracate se na dashboard", MessageType.Note);
                    return false;
                default:
                    Printer.ConfirmMessageAndClear("Krivi unos!", MessageType.Error);
                    return true;
            }
            return true;
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
                    return false;
                default:
                    Printer.ConfirmMessageAndClear("Krivi unos!", MessageType.Error);
                    return true;
            }
            return true;
        }

        private static void EditPassword()
        {
            var userQuery = new UserQueries();
            Console.WriteLine("Upisite novo korisničko ime[MIN 5 znakova], ostavite prazno polje ako zelite odustati:");
            var validInput = Checkers.CheckString(Console.ReadLine().Trim(), out string result);
            userQuery.EditPassword(result);
        }

        private static void EditUserName()
        {
            var userQuery = new UserQueries();
            Console.WriteLine("Upisite novu lozinku[MIN 5 znakova], ostavite prazno polje ako zelite odustati:");
            var validInput = Checkers.CheckString(Console.ReadLine().Trim(), out string result);
            userQuery.EditUserName(result);
        }

        private static void Reactivate()
        {
            var userQuery = new UserQueries();

            Console.WriteLine("Upisite ID korisnika:");
            var userId = int.Parse(Console.ReadLine());

            userQuery.Reactivate(userId);
        }

        private static void PermaDeactivate()
        {
            var userQuery = new UserQueries();

            Console.WriteLine("Upisite ID korisnika:");
            var userId = int.Parse(Console.ReadLine());

            userQuery.PermaDeactivate(userId);
        }

        private static void TemporaryDeactivate()
        {
            var userQuery = new UserQueries();

            Console.WriteLine("Upisite ID korisnika:");
            var userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Upiste broj dana za deaktivaciju:");
            var numberOfDays = int.Parse(Console.ReadLine());

            userQuery.TemporaryDeactivate(userId, numberOfDays);
        }
    }
}
