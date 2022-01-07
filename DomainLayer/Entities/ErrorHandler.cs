namespace DomainLayer.Entities
{
    public static class ErrorHandler
    {
        public static bool PrintError(bool validInput, bool noPoints, bool doesExist, bool validEntity)
        {
            if (validInput is false)
            {
                Console.WriteLine("Neispravan unos ID-a");
                Console.ReadKey();
                return false;
            }

            if(doesExist is false)
            {
                Console.WriteLine("Resurs ne postoji");
                Console.ReadKey();
                return false;
            }

            if (validEntity is false)
            {
                Console.WriteLine("Akcija nad entitetom neuspjela");
                Console.ReadKey();
                return false;
            }

            if (noPoints)
            {
                Console.WriteLine("Nemate dovoljno bodova za ovu akciju");
                Console.ReadKey();
                return false;
            }

            Console.WriteLine("Akcija uspjesno izvedena");
            Console.ReadKey();
            return true;
        }

        public static void ErrorCode()
        {

        }
    }
}
