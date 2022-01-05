using DomainLayer.Queries;

namespace PresentationLayer.Entities
{
    public static class UserInteract
    {
        public static void StartInteract()
        {
            var validInput = false;

            do
            {
                Printer.PrintUserInteractMenu();

                validInput = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

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
                    default:
                        Printer.ConfirmMessage("Krivi unos!");
                        break;
                }
            }
            while (!validInput);
        }

        public static void StartSettings()
        {
            var validInput = false;

            do
            { 

                validInput = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

                switch (menuOption)
                {
                    case 1:
                        EditUserName();
                        break;
                    case 2:
                        EditPassword();
                        break;
                    default:
                        Printer.ConfirmMessage("Krivi unos!");
                        break;
                }
            }
            while (!validInput);
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
